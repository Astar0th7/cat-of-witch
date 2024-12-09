using UnityEngine;

namespace Gameplay.Model.Enemy
{
    [CreateAssetMenu(fileName = "New Enemy", menuName = "StaticData/Enemy")]
    public class EnemyStaticData : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _nameKey;
        [SerializeField] private int _health;
        
        public string Id => _id;
        public string NameKey => _nameKey;
        public int Health => _health;
    }
}