using R3;
using UnityEngine;

namespace Gameplay.Model
{
    public class PlayerBattleProxy
    {
        public ReactiveProperty<int> CurrentHealth { get; }
        public ReactiveProperty<int> MaxHealth { get; }
        public ReactiveProperty<int> Energy { get; }
        public ReactiveProperty<int> NumberOfCart { get; }
        
        public PlayerBattleProxy(PlayerBattleData playerBattleData)
        {
            CurrentHealth = new ReactiveProperty<int>(playerBattleData.CurrentHealth);
            MaxHealth = new ReactiveProperty<int>(playerBattleData.MaxHealth);
            Energy = new ReactiveProperty<int>(playerBattleData.Energy);
            NumberOfCart = new ReactiveProperty<int>(playerBattleData.NumberOfCart);
            
            CurrentHealth.Skip(1).Subscribe(value => playerBattleData.CurrentHealth = value);
            MaxHealth.Skip(1).Subscribe(value => playerBattleData.MaxHealth = value);
            Energy.Skip(1).Subscribe(value => playerBattleData.Energy = value);
            NumberOfCart.Skip(1).Subscribe(value => playerBattleData.NumberOfCart = value);
        }

        public bool TryCast(int cost)
        {
            if (Energy.Value < cost) 
                return false;
            
            Energy.Value -= cost;
            return true;

        }

        public void Healing(int health)
        {
            CurrentHealth.Value = Mathf.Min(MaxHealth.Value, CurrentHealth.Value + health);
        }

        public void TakeDamage(int damage)
        {
            CurrentHealth.Value = Mathf.Max(0, CurrentHealth.Value - damage);
        }
    }
}