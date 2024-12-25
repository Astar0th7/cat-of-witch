using System;
using System.Collections.Generic;
using R3;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Model.Enemies
{
    public class EnemyProxy
    {
        private readonly EnemyData _originData;
        private readonly List<Ability> _abilities;
        
        public string Id { get; }
        public int MaxHealth { get; }
        public ReactiveProperty<int> CurrentHealth { get; }
        
        public event Action EnemyDieEvent;
        
        public EnemyProxy(EnemyData originData, List<Ability> abilities)
        {
            Id = originData.Id;
            MaxHealth = originData.MaxHealth;
            _originData = originData;
            _abilities = abilities;
            
            CurrentHealth = new ReactiveProperty<int>(_originData.CurrentHealth);
            CurrentHealth.Skip(1).Subscribe(value => _originData.CurrentHealth = value);
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth.Value = Mathf.Max(0, CurrentHealth.Value - damage);
            
            if (CurrentHealth.Value <= 0)
                EnemyDieEvent?.Invoke();
        }

        public void Heal(int health)
        {
            CurrentHealth.Value = Mathf.Min(MaxHealth, CurrentHealth.Value + health);
        }

        public void UseAbility(PlayerBattleProxy player)
        {
            _abilities[Random.Range(0, _abilities.Count)].Apply(player);
        }
    }
}