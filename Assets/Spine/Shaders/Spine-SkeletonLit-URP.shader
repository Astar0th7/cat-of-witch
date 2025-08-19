Shader "Universal Render Pipeline/2D/Spine/Skeleton Lit Patch"
{
    Properties
    {
        [NoScaleOffset] _MainTex ("Main Texture", 2D) = "black" {}
        [NoScaleOffset] _MaskTex ("Mask", 2D) = "white" {}
        [Toggle(_STRAIGHT_ALPHA_INPUT)] _StraightAlphaInput ("Straight Alpha Texture", Int) = 0
        [HideInInspector] _StencilRef ("Stencil Reference", Float) = 1.0
        [Enum(UnityEngine.Rendering.CompareFunction)] _StencilComp ("Stencil Compare", Float) = 0.0    // Disabled by default
    }

    HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    ENDHLSL

    SubShader
    {
        Tags { "Queue" = "Transparent"
               "RenderType" = "Transparent"
               "RenderPipeline" = "UniversalPipeline"
               "IgnoreProjector" = "True" }
        Cull Off
        ZWrite Off

        Stencil
        {
            Ref   [_StencilRef]
            Comp  [_StencilComp]
            Pass  Keep
        }

    /*──────────────────────────────────────────────────────────────────────────
      PASS 0 ─ LIT (Universal2D)
    ──────────────────────────────────────────────────────────────────────────*/
        Pass
        {
            Tags { "LightMode" = "Universal2D" }

            ZWrite Off
            Cull Off
            Blend One OneMinusSrcAlpha

            HLSLPROGRAM
                #pragma prefer_hlslcc gles
                #pragma exclude_renderers d3d11_9x
                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_0 __
                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_1 __
                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_2 __
                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_3 __

                struct Attributes
                {
                    float3 positionOS : POSITION;
                    half4  color      : COLOR;
                    float2 uv         : TEXCOORD0;

                    // (Optional) For SRP Batcher / GPU instancing:
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct Varyings
                {
                    float4 positionCS : SV_POSITION;
                    half4  color      : COLOR0;
                    float2 uv         : TEXCOORD0;
                    float2 lightingUV : TEXCOORD1;

                    UNITY_VERTEX_INPUT_INSTANCE_ID // keep instance data
                };

                // ─ Spine keyword
                #pragma shader_feature _ _STRAIGHT_ALPHA_INPUT
                #pragma vertex   CombinedShapeLightVertex
                #pragma fragment CombinedShapeLightFragment

                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/LightingUtility.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/SurfaceData2D.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/InputData2D.hlsl"

                TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
                TEXTURE2D(_MaskTex); SAMPLER(sampler_MaskTex);

                /*──── Shape-light macros (up to 4) ─────────────────────────*/
                #if USE_SHAPE_LIGHT_TYPE_0
                    SHAPE_LIGHT(0)
                #endif
                #if USE_SHAPE_LIGHT_TYPE_1
                    SHAPE_LIGHT(1)
                #endif
                #if USE_SHAPE_LIGHT_TYPE_2
                    SHAPE_LIGHT(2)
                #endif
                #if USE_SHAPE_LIGHT_TYPE_3
                    SHAPE_LIGHT(3)
                #endif
                /*───────────────────────────────────────────────────────────*/

                Varyings CombinedShapeLightVertex (Attributes v)
                {
                    Varyings o = (Varyings)0;
                    UNITY_SETUP_INSTANCE_ID(v);    // for SRP Batcher

                    o.positionCS = TransformObjectToHClip(v.positionOS);
                    o.uv         = v.uv;

                    float4 clipPos = o.positionCS / o.positionCS.w;
                    o.lightingUV   = ComputeScreenPos(clipPos).xy;

                    o.color = v.color;
                    return o;
                }

                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/CombinedShapeLightShared.hlsl"

                half4 CombinedShapeLightFragment (Varyings i) : SV_Target
                {
                    UNITY_SETUP_INSTANCE_ID(i);

                    /*── Sample texture + tint ────────────────────────────*/
                    half4 tex  = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                    #if defined(_STRAIGHT_ALPHA_INPUT)
                        tex.rgb *= tex.a;
                    #endif
                    half4 main = tex * i.color;
                    if (main.a == 0) return main;

                    half4 mask = SAMPLE_TEXTURE2D(_MaskTex, sampler_MaskTex, i.uv);

                    /*── Build new URP-17 data structs ───────────────────*/
                    SurfaceData2D surfaceData;
                    InputData2D   inputData;

                    InitializeSurfaceData(main.rgb,         // albedo
                                          main.a,          // alpha
                                          mask,            // mask
                                          surfaceData);

                    InitializeInputData (i.uv,
                                         i.lightingUV,
                                         inputData);

                    /*── Final lighting ──────────────────────────────────*/
                    return CombinedShapeLightShared(surfaceData, inputData);
                }
            ENDHLSL
        }

    /*──────────────────────────────────────────────────────────────────────────
      PASS 1 ─ NORMALS (for 2D normal maps)
    ──────────────────────────────────────────────────────────────────────────*/
        Pass
        {
            Tags { "LightMode" = "NormalsRendering" }

            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            HLSLPROGRAM
                #pragma prefer_hlslcc gles
                #pragma vertex   NormalsRenderingVertex
                #pragma fragment NormalsRenderingFragment

                struct Attributes
                {
                    float3 positionOS : POSITION;
                    float4 color      : COLOR;
                    float2 uv         : TEXCOORD0;
                };

                struct Varyings
                {
                    float4 positionCS : SV_POSITION;
                    float4 color      : COLOR;
                    float2 uv         : TEXCOORD0;
                    float3 normalVS   : TEXCOORD1;
                };

                TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);

                Varyings NormalsRenderingVertex (Attributes a)
                {
                    Varyings o =(Varyings)0;
                    o.positionCS = TransformObjectToHClip(a.positionOS);
                    o.uv         = a.uv;
                    o.color      = a.color;

                    float3 normalWS = TransformObjectToWorldDir(float3(0,0,-1));
                    o.normalVS      = TransformWorldToViewDir(normalWS);
                    return o;
                }

                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/NormalsRenderingShared.hlsl"

                float4 NormalsRenderingFragment (Varyings i) : SV_Target
                {
                    float4 mainTex = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                    float4 normalColor;
                    normalColor.rgb = 0.5 * (i.normalVS + 1);
                    normalColor.a   = mainTex.a;
                    return normalColor;
                }
            ENDHLSL
        }

    /*──────────────────────────────────────────────────────────────────────────
      PASS 2 ─ UNLIT fallback (no 2D lights)
    ──────────────────────────────────────────────────────────────────────────*/
        Pass
        {
            Name  "Unlit"
            Tags { "LightMode" = "UniversalForward"
                   "Queue"="Transparent"
                   "RenderType"="Transparent" }

            ZWrite Off
            Cull Off
            Blend One OneMinusSrcAlpha

            HLSLPROGRAM
                #pragma prefer_hlslcc gles
                #pragma vertex   UnlitVertex
                #pragma fragment UnlitFragment

                struct Attributes
                {
                    float3 positionOS : POSITION;
                    float4 color      : COLOR;
                    float2 uv         : TEXCOORD0;
                };

                struct Varyings
                {
                    float4 positionCS : SV_POSITION;
                    float4 color      : COLOR;
                    float2 uv         : TEXCOORD0;
                };

                TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);

                Varyings UnlitVertex (Attributes a)
                {
                    Varyings o = (Varyings)0;
                    o.positionCS = TransformObjectToHClip(a.positionOS);
                    o.uv         = a.uv;
                    o.color      = a.color;
                    return o;
                }

                float4 UnlitFragment (Varyings i) : SV_Target
                {
                    half4 tex  = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                    half4 main;

                    #if defined(_STRAIGHT_ALPHA_INPUT)
                        main.rgb = tex.rgb * i.color.rgb * tex.a;
                    #else
                        main.rgb = tex.rgb * i.color.rgb;
                    #endif

                    main.a = tex.a * i.color.a;
                    return main;
                }
            ENDHLSL
        }
    }
    /*──────────────────────────────────────────────────────────────────────────*/
    FallBack "Universal Render Pipeline/2D/Sprite-Lit-Default"
}
