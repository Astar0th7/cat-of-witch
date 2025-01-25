using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View
{
    public class UIBattlePauseView : MonoBehaviour
    {
        [SerializeField] private Button _restartButton;
        [SerializeField] private Button _libraryButton;
        [SerializeField] private Button _leaveButton;
        [SerializeField] private float _durationButtonAnimation = 0.5f;

        public event Action RestartButtonClickedEvent;
        public event Action LibraryButtonClickedEvent;
        public event Action LeaveButtonClickedEvent;

        public bool IsActive => gameObject.activeSelf;

        private Sequence _showSequence;
        private Sequence _hideSequence;

        private void OnEnable()
        {
            _restartButton.onClick.AddListener(OnRestartButtonClicked);
            _libraryButton.onClick.AddListener(OnLibraryButtonClicked);
            _leaveButton.onClick.AddListener(OnLeaveButtonClicked);
        }

        private void OnDisable()
        {
            _restartButton.onClick.RemoveListener(OnRestartButtonClicked);
            _libraryButton.onClick.RemoveListener(OnLibraryButtonClicked);
            _leaveButton.onClick.RemoveListener(OnLeaveButtonClicked);
            
            _showSequence?.Kill();
            _hideSequence?.Kill();
        }

        public void Show()
        {
            _hideSequence?.Kill();
            
            _restartButton.transform.localScale = Vector3.zero;
            _libraryButton.transform.localScale = Vector3.zero;
            _leaveButton.transform.localScale = Vector3.zero;
            gameObject.SetActive(true);

            _showSequence = DOTween.Sequence();
            _showSequence.Append(_restartButton.transform.DOScale(Vector3.one, _durationButtonAnimation).SetEase(Ease.OutBack));
            _showSequence.Append(_libraryButton.transform.DOScale(Vector3.one, _durationButtonAnimation).SetEase(Ease.OutBack));
            _showSequence.Append(_leaveButton.transform.DOScale(Vector3.one, _durationButtonAnimation).SetEase(Ease.OutBack));
            _showSequence.Play();
        }

        public void Hide()
        {
            _showSequence?.Kill(true);
            _hideSequence = DOTween.Sequence();
            _hideSequence.Append(_restartButton.transform.DOScale(Vector3.zero, _durationButtonAnimation).SetEase(Ease.InBack));
            _hideSequence.Append(_libraryButton.transform.DOScale(Vector3.zero, _durationButtonAnimation).SetEase(Ease.InBack));
            _hideSequence.Append(_leaveButton.transform.DOScale(Vector3.zero, _durationButtonAnimation).SetEase(Ease.InBack));
            _hideSequence.Play().OnComplete(() => gameObject.SetActive(false));
        }

        private void OnRestartButtonClicked() => 
            RestartButtonClickedEvent?.Invoke();

        private void OnLibraryButtonClicked() => 
            LibraryButtonClickedEvent?.Invoke();

        private void OnLeaveButtonClicked() => 
            LeaveButtonClickedEvent?.Invoke();
    }
}