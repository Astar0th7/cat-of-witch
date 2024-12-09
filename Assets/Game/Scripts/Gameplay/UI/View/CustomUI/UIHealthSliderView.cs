using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View
{
    public class UIHealthSliderView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private Slider _slider;

        public void SetHealth(int currentHealth, int maxHealth)
        {
            _amountText.text = currentHealth.ToString();
            _slider.value = (float)currentHealth / maxHealth;
        }
    }
}