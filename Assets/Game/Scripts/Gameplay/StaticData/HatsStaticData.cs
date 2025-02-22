using System.Collections.Generic;
using UnityEngine;

namespace Model.StaticData
{
    [CreateAssetMenu(fileName = "Hats", menuName = "StaticData/Hats")]
    public class HatsStaticData : ScriptableObject
    {
        public List<HatData> Hats;
    }
}