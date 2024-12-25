namespace Gameplay.Services
{
    public interface IGameplayStateService
    {
        GameplayState State { get; }
        void HomeGameplay();
        void BattleGameplay();
    }
}