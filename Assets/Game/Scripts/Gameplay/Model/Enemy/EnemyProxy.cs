using R3;

namespace Gameplay.Model.Enemy
{
    public class EnemyProxy
    {
        public string Id { get; }
        public string NameKey { get; }
        public int MaxHealth { get; }
        public ReactiveProperty<int> CurrentHealth { get; }
        
        public EnemyProxy(EnemyData originData)
        {
            Id = originData.Id;
            NameKey = originData.NameKey;
            MaxHealth = originData.MaxHealth;
            CurrentHealth = new ReactiveProperty<int>(originData.CurrentHealth);
            CurrentHealth.Skip(1).Subscribe(value => originData.CurrentHealth = value);
        }
    }
}