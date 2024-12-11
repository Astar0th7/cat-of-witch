using R3;
using UnityEngine;

namespace Gameplay.Model.Enemy
{
    public class EnemyProxy
    {
        private readonly EnemyData _originData;
        
        public string Id { get; }
        public string NameKey { get; }
        public int MaxHealth { get; }
        public ReactiveProperty<int> CurrentHealth { get; }
        
        public EnemyProxy(EnemyData originData)
        {
            Id = originData.Id;
            NameKey = originData.NameKey;
            MaxHealth = originData.MaxHealth;
            _originData = originData;
            CurrentHealth = new ReactiveProperty<int>(_originData.CurrentHealth);
            CurrentHealth.Skip(1).Subscribe(value => _originData.CurrentHealth = value);
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth.Value = Mathf.Max(0, CurrentHealth.Value - damage);
        }
    }
}