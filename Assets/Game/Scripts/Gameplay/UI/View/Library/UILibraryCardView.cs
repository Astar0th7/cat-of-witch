using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View.Library
{
    public class UILibraryCardView : MonoBehaviour
    {
        [SerializeField] private Button _cardButton;
        [SerializeField] private Image _backgroundImage;
        [SerializeField] private Image _iconImage;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Image _otherStatsImage;
        [SerializeField] private TMP_Text _otherStatsText;
        [SerializeField] private TMP_Text _costText;
        [SerializeField] private TMP_Text _descriptionText;

        public event Action CardButtonClickedEvent;
        
        private void OnEnable()
        {
            _cardButton.onClick.AddListener(OnCardButtonClicked);
        }

        private void OnDisable()
        {
            _cardButton.onClick.RemoveListener(OnCardButtonClicked);
        }

        public void SetCardData(
            Sprite background, 
            Sprite icon, 
            string nameCard, 
            Sprite otherStatsSprite, 
            int otherStats,
            int cost, 
            string description)
        {
            _backgroundImage.sprite = background;
            _iconImage.sprite = icon;
            _nameText.text = nameCard;
            
            if (otherStatsSprite == null)
            {
                _otherStatsImage.gameObject.SetActive(false);
                _otherStatsText.gameObject.SetActive(false);
            }
            
            _otherStatsImage.sprite = otherStatsSprite;
            _otherStatsText.text = otherStats.ToString();
            _costText.text = cost.ToString();
            _descriptionText.text = description;
        }

        private void OnCardButtonClicked() => 
            CardButtonClickedEvent?.Invoke();
    }
}