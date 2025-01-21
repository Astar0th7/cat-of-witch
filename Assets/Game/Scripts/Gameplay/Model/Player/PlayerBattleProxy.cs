using Model;
using Model.Entities;
using R3;
using UnityEngine;

namespace Gameplay.Model
{
    public class PlayerBattleProxy : IEntity
    {
        public int MaxHealth { get; }
        public int MaxEnergy { get; }
        public ReactiveProperty<int> CurrentHealth { get; }
        public ReactiveProperty<int> CurrentEnergy { get; }
        public ReactiveProperty<int> NumberOfCart { get; }
        public TimingEffect TimingEffects { get; }

        public PlayerBattleProxy(PlayerBattleData playerBattleData)
        {
            MaxHealth = playerBattleData.MaxHealth;
            MaxEnergy = playerBattleData.MaxEnergy;

            CurrentHealth = new ReactiveProperty<int>(playerBattleData.CurrentHealth);
            CurrentEnergy = new ReactiveProperty<int>(playerBattleData.CurrentEnergy);
            NumberOfCart = new ReactiveProperty<int>(playerBattleData.NumberOfCart);
            TimingEffects = new TimingEffect(); 
            
            CurrentHealth.Skip(1).Subscribe(value => playerBattleData.CurrentHealth = value);
            CurrentEnergy.Skip(1).Subscribe(value => playerBattleData.CurrentEnergy = value);
            NumberOfCart.Skip(1).Subscribe(value => playerBattleData.NumberOfCart = value);
        }

        public bool TryCast(int cost)
        {
            if (CurrentEnergy.Value < cost) 
                return false;
            
            CurrentEnergy.Value -= cost;
            return true;

        }

        public void Heal(int health)
        {
            CurrentHealth.Value = Mathf.Min(MaxHealth, CurrentHealth.Value + health);
        }

        public void AddEnergy(int energy)
        {
            CurrentEnergy.Value = Mathf.Min(MaxEnergy, CurrentEnergy.Value + energy);
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth.Value = Mathf.Max(0, CurrentHealth.Value - damage);
        }
    }
}