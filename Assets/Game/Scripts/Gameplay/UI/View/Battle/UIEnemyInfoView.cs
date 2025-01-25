using TMPro;
using UnityEngine;

namespace Gameplay.UI.View
{
    public class UIEnemyInfoView : MonoBehaviour
    {
        [SerializeField] private UISliderView _healthSliderView;
        [SerializeField] private UITimingEffectsView _timingEffectsView;
        
        public UISliderView HealthSliderView => _healthSliderView;
        public UITimingEffectsView TimingEffectsView => _timingEffectsView;
    }
}