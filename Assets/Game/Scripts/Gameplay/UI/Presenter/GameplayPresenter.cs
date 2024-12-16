using Gameplay.UI.View;

namespace Gameplay.UI.Presenter
{
    public class GameplayPresenter
    {
        private readonly View.UIGameplayView _view;

        public GameplayPresenter(View.UIGameplayView view)
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