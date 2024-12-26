using Gameplay.UI.View;

namespace Gameplay.UI.Presenter.CustomUI
{
    public class SliderPresenter
    {
        private readonly UISliderView _view;

        public SliderPresenter(UISliderView view)
        {
            _view = view;
        }

        public void SetValue(int current, int max)
        {
            _view.SetValue(current, max);
        }
    }
}