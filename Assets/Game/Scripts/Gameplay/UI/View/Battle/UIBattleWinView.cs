using System;
using Root.UI.View;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View
{
    public class UIBattleWinView : BaseView
    {
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private Image _rewardImage;
        [SerializeField] private Button _continueButton;

        public event Action ContinueButtonClickedEvent;
        
        private void OnEnable()
        {
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        private void OnDisable()
        {
            _continueButton.onClick.RemoveListener(OnContinueButtonClicked);
        }

        public void SetReward(int amount, Sprite icon)
        {
            _amountText.text = amount.ToString();
            _rewardImage.sprite = icon;
        }

        private void OnContinueButtonClicked()
        {
            ContinueButtonClickedEvent?.Invoke();
        }
    }
}