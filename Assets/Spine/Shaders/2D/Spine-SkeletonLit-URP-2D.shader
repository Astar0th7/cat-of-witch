Shader "Universal Render Pipeline/2D/Spine/Skeleton Lit Patch Outline"
{
    /*───────────────────────────
      1.  PROPERTIES
    ───────────────────────────*/
    Properties
    {
        [NoScaleOffset] _MainTex        ("Main Texture",           2D)   = "black" {}
        [NoScaleOffset] _MaskTex        ("Mask",                  2D)   = "white" {}
        [Toggle(_STRAIGHT_ALPHA_INPUT)] _StraightAlphaInput       ("Straight Alpha Texture", Int) = 0
        /*─ NEW ─*/ _OutlineColor       ("Outline Color",          Color) = (0,0,0,1)
        /*─ NEW ─*/ _OutlineWidthPx     ("Outline Width (px)",     Range(0,10)) = 1
        [HideInInspector] _StencilRef   ("Stencil Reference",     Float) = 1.0
        [Enum(UnityEngine.Rendering.CompareFunction)]
                         _StencilComp   ("Stencil Compare",       Float) = 0.0   // Disabled by default
    }

    /*───────────────────────────
      2.  GLOBAL INCLUDES
    ───────────────────────────*/
    HLSLINCLUDE
        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    ENDHLSL


    SubShader
    {
        Tags { "Queue"           = "Transparent"
               "RenderType"      = "Transparent"
               "RenderPipeline"  = "UniversalPipeline"
               "IgnoreProjector" = "True" }
        Cull Off
        ZWrite Off

        /*──── Stencil settings (unchanged) ────*/
        Stencil { Ref [_StencilRef] Comp [_StencilComp] Pass Keep }

    /*──────────────────────────────────────────
      PASS 0 — LIT (Universal2D)   +  OUTLINE
    ──────────────────────────────────────────*/
        Pass
        {
            Tags { "LightMode" = "Universal2D" }

            ZWrite Off
            Cull Off
            Blend One OneMinusSrcAlpha

            HLSLPROGRAM
                /*──── Pragmas ────*/
                #pragma prefer_hlslcc gles
                #pragma exclude_renderers d3d11_9x
                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_0 __
                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_1 __
                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_2 __
                #pragma multi_compile USE_SHAPE_LIGHT_TYPE_3 __
                #pragma shader_feature _ _STRAIGHT_ALPHA_INPUT
                #pragma vertex   CombinedShapeLightVertex
                #pragma fragment CombinedShapeLightFragment

                /*──── Structs ────*/
                struct Attributes
                {
                    float3 positionOS : POSITION;
                    half4  color      : COLOR;
                    float2 uv         : TEXCOORD0;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                struct Varyings
                {
                    float4 positionCS : SV_POSITION;
                    half4  color      : COLOR0;
                    float2 uv         : TEXCOORD0;
                    float2 lightingUV : TEXCOORD1;
                    UNITY_VERTEX_INPUT_INSTANCE_ID
                };

                /*──── Built-in includes ────*/
                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/LightingUtility.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/SurfaceData2D.hlsl"
                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/InputData2D.hlsl"
                #include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl" // _ScreenParams

                /*──── Textures & uniforms ────*/
                TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);
                TEXTURE2D(_MaskTex); SAMPLER(sampler_MaskTex);

                float4 _OutlineColor;
                float  _OutlineWidthPx;

                /*──── Shape-light macros (max 4) ────*/
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

                /*────────────────────────────
                  Vertex: unchanged
                ────────────────────────────*/
                Varyings CombinedShapeLightVertex (Attributes v)
                {
                    UNITY_SETUP_INSTANCE_ID(v);

                    Varyings o;
                    o.positionCS = TransformObjectToHClip(v.positionOS);
                    o.uv         = v.uv;
                    o.color      = v.color;

                    float4 clipPos   = o.positionCS / o.positionCS.w;
                    o.lightingUV     = ComputeScreenPos(clipPos).xy;
                    return o;
                }

                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/CombinedShapeLightShared.hlsl"

                /*────────────────────────────
                  Fragment: now with OUTLINE
                ────────────────────────────*/
                half4 CombinedShapeLightFragment (Varyings i) : SV_Target
                {
                    UNITY_SETUP_INSTANCE_ID(i);

                    /*── 1. Base sprite sample ─────────────────────────*/
                    half4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                    #if defined(_STRAIGHT_ALPHA_INPUT)
                        tex.rgb *= tex.a;
                    #endif
                    half4 main = tex * i.color;

                    /*── 2. Outline detection (if pixel transparent) ───*/
                    float2 pixelSize = 1.0 / _ScreenParams.xy;          // uv size of 1 px
                    float2 uvStep    = _OutlineWidthPx * pixelSize;

                    // 4-direction cross sampling (L,R,U,D)
                    half aL = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv + float2(-uvStep.x, 0)).a;
                    half aR = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv + float2( uvStep.x, 0)).a;
                    half aU = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv + float2(0,  uvStep.y)).a;
                    half aD = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv + float2(0, -uvStep.y)).a;

                    bool isEdge = (main.a == 0) && (aL + aR + aU + aD > 0);

                    half4 surfColor = isEdge ? _OutlineColor : main;

                    /*── 3. Mask + lighting chain ─────────────────────*/
                    half4 mask = SAMPLE_TEXTURE2D(_MaskTex, sampler_MaskTex, i.uv);

                    SurfaceData2D surfaceData;
                    InputData2D   inputData;
                    InitializeSurfaceData(surfColor.rgb, surfColor.a, mask, surfaceData);
                    InitializeInputData (i.uv, i.lightingUV, inputData);

                    return CombinedShapeLightShared(surfaceData, inputData);
                }
            ENDHLSL
        }

    /*──────────────────────────────────────────
      PASS 1 — NORMALS (unchanged)
    ──────────────────────────────────────────*/
        Pass
        {
            Tags { "LightMode" = "NormalsRendering" }
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off

            HLSLPROGRAM
                #pragma prefer_hlslcc gles
                #pragma vertex   NormalsRenderingVertex
                #pragma fragment NormalsRenderingFragment

                struct Attributes { float3 positionOS:POSITION; float4 color:COLOR; float2 uv:TEXCOORD0; };
                struct Varyings   { float4 positionCS:SV_POSITION; float4 color:COLOR; float2 uv:TEXCOORD0; float3 normalVS:TEXCOORD1; };

                TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);

                Varyings NormalsRenderingVertex (Attributes a)
                {
                    Varyings o;
                    o.positionCS = TransformObjectToHClip(a.positionOS);
                    o.uv         = a.uv;
                    o.color      = a.color;

                    float3 nWS  = TransformObjectToWorldDir(float3(0,0,-1));
                    o.normalVS  = TransformWorldToViewDir(nWS);
                    return o;
                }

                #include "Packages/com.unity.render-pipelines.universal/Shaders/2D/Include/NormalsRenderingShared.hlsl"

                float4 NormalsRenderingFragment (Varyings i) : SV_Target
                {
                    float4 base = i.color * SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                    return float4(0.5 * (i.normalVS + 1), base.a);
                }
            ENDHLSL
        }

    /*──────────────────────────────────────────
      PASS 2 — UNLIT (unchanged)
    ──────────────────────────────────────────*/
        Pass
        {
            Name  "Unlit"
            Tags { "LightMode"="UniversalForward" "Queue"="Transparent" "RenderType"="Transparent" }

            ZWrite Off
            Cull Off
            Blend One OneMinusSrcAlpha

            HLSLPROGRAM
                #pragma prefer_hlslcc gles
                #pragma vertex   UnlitVertex
                #pragma fragment UnlitFragment

                struct Attributes { float3 positionOS:POSITION; float4 color:COLOR; float2 uv:TEXCOORD0; };
                struct Varyings   { float4 positionCS:SV_POSITION; float4 color:COLOR; float2 uv:TEXCOORD0; };

                TEXTURE2D(_MainTex); SAMPLER(sampler_MainTex);

                Varyings UnlitVertex (Attributes a)
                {
                    Varyings o;
                    o.positionCS = TransformObjectToHClip(a.positionOS);
                    o.uv         = a.uv;
                    o.color      = a.color;
                    return o;
                }

                float4 UnlitFragment (Varyings i) : SV_Target
                {
                    half4 tex = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.uv);
                    #if defined(_STRAIGHT_ALPHA_INPUT)
                        tex.rgb *= tex.a;
                    #endif
                    half4 col;
                    col.rgb = tex.rgb * i.color.rgb;
                    col.a   = tex.a   * i.color.a;
                    return col;
                }
            ENDHLSL
        }
    }

    /*──────────────────────────────────────────*/
    FallBack "Universal Render Pipeline/2D/Sprite-Lit-Default"
}
