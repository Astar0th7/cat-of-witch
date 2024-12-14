namespace Gameplay.Model.Enemies
{
    public abstract class Ability
    {
        public EnemyProxy Enemy { get; }
        public EnemyStaticData StaticData { get; }

        public Ability(EnemyProxy enemy, EnemyStaticData staticData)
        {
            Enemy = enemy;
            StaticData = staticData;
        }
        
        public abstract void Apply(PlayerBattleProxy player);
    }
}