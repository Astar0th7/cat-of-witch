namespace Gameplay.Services
{
    public interface IGameplayListener
    {
        
    }

    public interface IHomeGameplayListener : IGameplayListener
    {
        void OnHomeGameplay();
    }
    
    public interface IBattleGameplayListener : IGameplayListener
    {
        void OnBattleGameplay();
    }
}