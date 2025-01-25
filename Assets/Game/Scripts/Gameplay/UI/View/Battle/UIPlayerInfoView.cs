using TMPro;
using UnityEngine;

namespace Gameplay.UI.View
{
    public class UIPlayerInfoView : MonoBehaviour
    {
        [SerializeField] private UISliderView _healthSlider;
        [SerializeField] private UISliderView _energySlider;
        [SerializeField] private UITimingEffectsView _timingEffectsView;

        public UISliderView HealthSlider => _healthSlider;
        public UISliderView EnergySlider => _energySlider;
        public UITimingEffectsView TimingEffectsView => _timingEffectsView;
    }
}