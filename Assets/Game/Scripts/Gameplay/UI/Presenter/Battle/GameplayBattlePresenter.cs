using Gameplay.Services;
using Gameplay.UI.View;

namespace Gameplay.UI.Presenter
{
    public class GameplayBattlePresenter : IHomeGameplayListener, IBattleGameplayListener
    {
        private readonly UIGameplayBattleView _view;

        public GameplayBattlePresenter(UIGameplayBattleView view)
        {
            _view = view;
        }

        public void OnBattleGameplay()
        {
            Show();
        }

        public void OnHomeGameplay()
        {
            Hide();
        }

        public void Show()
        {
            _view.Show();
        }

        public void Hide()
        {
            _view.Hide();
        }
    }
}