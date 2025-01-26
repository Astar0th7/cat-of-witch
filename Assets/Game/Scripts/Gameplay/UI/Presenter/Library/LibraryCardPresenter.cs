using System;
using Gameplay.Model;
using Gameplay.UI.View.Library;
using Root.Services.Translation;
using UnityEngine;
using Zenject;

namespace Gameplay.UI.Presenter.Library
{
    public class LibraryCardPresenter : IInitializable, IDisposable
    {
        private readonly UILibraryCardView _view;
        private readonly CardStaticData _cardData;
        private readonly ITranslationService _translationService;

        public string Id => _cardData.Id;

        public LibraryCardPresenter(
            UILibraryCardView view, 
            CardStaticData cardData, 
            ITranslationService translationService)
        {
            _view = view;
            _cardData = cardData;
            _translationService = translationService;
            
            SetCardData();
        }

        public void Initialize()
        {
            _view.CardButtonClickedEvent += OnCardButtonClicked;
        }

        public void Dispose()
        {
            _view.CardButtonClickedEvent -= OnCardButtonClicked;
        }

        private async void SetCardData()
        {
            string nameCard = await _translationService.Translate($"{_cardData.Id}_name", NameTables.CARDS);
            int stats = _cardData.GetStats();
            Sprite statIcon = _cardData.GetStatIcon();
            string description = await _translationService.Translate($"{_cardData.Id}_description", NameTables.CARDS);

            _view.SetCardData(_cardData.Background, _cardData.Icon, nameCard, statIcon, stats, _cardData.Cost, description);
        }
        
        private void OnCardButtonClicked()
        {
        }
    }
}