using Gameplay.UI.View;

namespace Gameplay.UI.Presenter
{
    public class GameplayBattlePresenter
    {
        private readonly UIGameplayBattleView _view;

        public GameplayBattlePresenter(UIGameplayBattleView view)
        {
            _view = view;
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