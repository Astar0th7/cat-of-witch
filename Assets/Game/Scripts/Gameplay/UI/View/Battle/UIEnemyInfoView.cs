using TMPro;
using UnityEngine;

namespace Gameplay.UI.View
{
    public class UIEnemyInfoView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private UISliderView _healthSliderView;
        
        public UISliderView HealthSliderView => _healthSliderView;

        public void SetName(string value)
        {
            _nameText.text = value;
        }
    }
}