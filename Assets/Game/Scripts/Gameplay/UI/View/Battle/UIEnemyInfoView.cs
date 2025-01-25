using TMPro;
using UnityEngine;

namespace Gameplay.UI.View
{
    public class UIEnemyInfoView : MonoBehaviour
    {
        [SerializeField] private UISliderView _healthSliderView;
        
        public UISliderView HealthSliderView => _healthSliderView;
    }
}