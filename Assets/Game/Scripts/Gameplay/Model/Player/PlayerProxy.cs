using R3;

namespace Gameplay.Model
{
    public class PlayerProxy
    {
        private readonly PlayerData _originalData;

        public ReactiveProperty<int> MaxHealth { get; }
        public ReactiveProperty<int> MaxEnergy { get; }

        public PlayerProxy(PlayerData playerData)
        {
            _originalData = playerData;
            
            MaxHealth = new ReactiveProperty<int>(playerData.MaxHealth);
            MaxEnergy = new ReactiveProperty<int>(playerData.MaxEnergy);
            
            MaxHealth.Skip(1).Subscribe(value => playerData.MaxHealth = value);
            MaxEnergy.Skip(1).Subscribe(value => playerData.MaxEnergy = value);
        }
    }
}