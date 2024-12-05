using TMPro;
using UnityEngine;

namespace Gameplay.UI.View
{
    public class UIGameplayHUDView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _healthText;
        [SerializeField] private TMP_Text _energyText;
        [SerializeField] private TMP_Text _numberOfCardText;

        public void ChangeHealth(int health)
        {
            _healthText.text = health.ToString();
        }

        public void ChangeEnergy(int energy)
        {
            _energyText.text = energy.ToString();
        }

        public void ChangeNumberOfCard(int numberOfCard)
        {
            _numberOfCardText.text = numberOfCard.ToString();
        }
    }
}