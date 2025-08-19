using System;
using System.Collections.Generic;
using Model;
using Model.Entities;
using R3;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Gameplay.Model.Enemies
{
    public class EnemyProxy : IEntity
    {
        private readonly EnemyData _originData;
        private readonly List<Ability> _abilities;

        public IReadOnlyList<Ability> Abilities => _abilities;
        public TimingEffect TimingEffects { get; }

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

            TimingEffects = new TimingEffect();
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

        public void UseAbility(IEntity player)
        {
            Ability ability = _abilities[Random.Range(0, _abilities.Count)];
            int modify = ability.Data.Kind switch
            {
                AbilityKind.Damage => _originData.Damage,
                AbilityKind.Healing => _originData.Heal,
                _ => 0
            };

            IEntity entity = ability.Data.BaseType == BaseAbilityType.Negative ? player : this;
            ability.Apply(entity, modify);
        }

        public void Destroy()
        {
            if (_originData.EnemyView == null)
                return;
            
            Object.Destroy(_originData.EnemyView.gameObject);
        }
    }
}