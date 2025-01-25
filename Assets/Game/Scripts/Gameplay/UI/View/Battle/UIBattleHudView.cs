using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View
{
    public class UIBattleHudView : MonoBehaviour
    {
        [SerializeField] private Button _endStepButton;

        public event Action EndStepClickedEvent;
        
        private void OnEnable()
        {
            _endStepButton.onClick.AddListener(OnEndStepButtonClicked);
        }

        private void OnDisable()
        {
            _endStepButton.onClick.RemoveListener(OnEndStepButtonClicked);
        }

        private void OnEndStepButtonClicked()
        {
            EndStepClickedEvent?.Invoke();
        }
    }
}