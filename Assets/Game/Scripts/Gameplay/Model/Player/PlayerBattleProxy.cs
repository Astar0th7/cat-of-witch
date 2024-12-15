using R3;
using UnityEngine;

namespace Gameplay.Model
{
    public class PlayerBattleProxy
    {
        public ReactiveProperty<int> CurrentHealth { get; }
        public ReactiveProperty<int> MaxHealth { get; }
        public ReactiveProperty<int> CurrentEnergy { get; }
        public ReactiveProperty<int> MaxEnergy { get; }
        public ReactiveProperty<int> NumberOfCart { get; }
        
        public PlayerBattleProxy(PlayerBattleData playerBattleData)
        {
            CurrentHealth = new ReactiveProperty<int>(playerBattleData.CurrentHealth);
            MaxHealth = new ReactiveProperty<int>(playerBattleData.MaxHealth);
            CurrentEnergy = new ReactiveProperty<int>(playerBattleData.CurrentEnergy);
            MaxEnergy = new ReactiveProperty<int>(playerBattleData.MaxEnergy);
            NumberOfCart = new ReactiveProperty<int>(playerBattleData.NumberOfCart);
            
            CurrentHealth.Skip(1).Subscribe(value => playerBattleData.CurrentHealth = value);
            MaxHealth.Skip(1).Subscribe(value => playerBattleData.MaxHealth = value);
            CurrentEnergy.Skip(1).Subscribe(value => playerBattleData.CurrentEnergy = value);
            MaxEnergy.Skip(1).Subscribe(value => playerBattleData.MaxEnergy = value);
            NumberOfCart.Skip(1).Subscribe(value => playerBattleData.NumberOfCart = value);
        }

        public bool TryCast(int cost)
        {
            if (CurrentEnergy.Value < cost) 
                return false;
            
            CurrentEnergy.Value -= cost;
            return true;

        }

        public void Healing(int health)
        {
            CurrentHealth.Value = Mathf.Min(MaxHealth.Value, CurrentHealth.Value + health);
        }

        public void AddEnergy(int energy)
        {
            CurrentEnergy.Value = Mathf.Min(MaxEnergy.Value, MaxEnergy.Value + energy);
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth.Value = Mathf.Max(0, CurrentHealth.Value - damage);
        }
    }
}