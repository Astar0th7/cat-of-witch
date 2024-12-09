using Gameplay.UI.View;

namespace Gameplay.UI.Presenter.CustomUI
{
    public class HealthSliderPresenter
    {
        private readonly UIHealthSliderView _view;

        public HealthSliderPresenter(UIHealthSliderView view)
        {
            _view = view;
        }

        public void SetHealth(int currentHealth, int maxHealth)
        {
            _view.SetHealth(currentHealth, maxHealth);
        }
    }
}