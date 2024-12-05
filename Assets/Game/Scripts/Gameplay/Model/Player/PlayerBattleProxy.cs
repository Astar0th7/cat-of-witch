using R3;

namespace Gameplay.Model
{
    public class PlayerBattleProxy
    {
        public ReactiveProperty<int> Health { get; }
        public ReactiveProperty<int> Energy { get; }
        public ReactiveProperty<int> NumberOfCart { get; }
        
        public PlayerBattleProxy(PlayerBattleData playerBattleData)
        {
            Health = new ReactiveProperty<int>(playerBattleData.Health);
            Energy = new ReactiveProperty<int>(playerBattleData.Energy);
            NumberOfCart = new ReactiveProperty<int>(playerBattleData.NumberOfCart);
            
            Health.Skip(1).Subscribe(value => playerBattleData.Health = value);
            Energy.Skip(1).Subscribe(value => playerBattleData.Energy = value);
            NumberOfCart.Skip(1).Subscribe(value => playerBattleData.NumberOfCart = value);
        }
    }
}