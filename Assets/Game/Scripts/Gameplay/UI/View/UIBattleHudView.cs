using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View
{
    public class UIBattleHudView : MonoBehaviour
    {
        [SerializeField] private Button _endStepButton;
        [SerializeField] private Button _menuButton;

        public event Action EndStepClickedEvent;
        public event Action MenuClickedEvent;
        
        private void OnEnable()
        {
            _endStepButton.onClick.AddListener(OnEndStepButtonClicked);
            _menuButton.onClick.AddListener(OnMenuButtonClicked);
        }

        private void OnDisable()
        {
            _endStepButton.onClick.RemoveListener(OnEndStepButtonClicked);
            _menuButton.onClick.RemoveListener(OnMenuButtonClicked);
        }

        private void OnEndStepButtonClicked()
        {
            EndStepClickedEvent?.Invoke();
        }

        private void OnMenuButtonClicked()
        {
            MenuClickedEvent?.Invoke();
        }
    }
}