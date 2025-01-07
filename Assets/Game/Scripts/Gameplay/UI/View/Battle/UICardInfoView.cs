using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View
{
    public class UICardInfoView : MonoBehaviour
    {
        [SerializeField] private GameObject _card;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _costText;
        [SerializeField] private Image _otherStatsImage;
        [SerializeField] private TMP_Text _otherStatsText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private Button _closeButton;

        private Tween _scaleTween;
        
        public event Action CloseButtonClickedEvent;

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(OnCloseButtonClicked);
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        }

        private void OnDestroy()
        {
            _scaleTween?.Kill();
        }

        public void Show(
            Sprite backgroundImage, 
            Sprite iconImage, 
            string nameCard, 
            string cost, 
            string description, 
            Sprite otherStatsImage, 
            string otherStatsText)
        {
            _backgroundImage.sprite = backgroundImage;
            _iconImage.sprite = iconImage;
            _nameText.text = nameCard;
            _costText.text = cost;
            _descriptionText.text = description;
            _otherStatsImage.sprite = otherStatsImage;
            _otherStatsText.text = otherStatsText;

            _scaleTween?.Kill(true);
            _card.transform.localScale = Vector3.zero;
            _card.SetActive(true);
            _scaleTween = _card.transform
                .DOScale(Vector3.one, 0.5f)
                .SetEase(Ease.OutBack);
        }

        public void Hide()
        {
            _scaleTween = _card.transform
                .DOScale(Vector3.zero, 0.5f)
                .SetEase(Ease.InBack)
                .OnComplete(() => _card.SetActive(false));
        }
        
        private void OnCloseButtonClicked()
        {
            CloseButtonClickedEvent?.Invoke();
        }
    }
}