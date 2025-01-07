using System;
using Gameplay.Model;
using Gameplay.Presenters;
using Gameplay.UI.View;
using Gameplay.View;
using Root.Services.Translation;
using Zenject;

namespace Gameplay.UI.Presenter
{
    public class CardInfoPresenter : IInitializable, IDisposable
    {
        private readonly UICardInfoView _view;
        private readonly ITranslationService _translationService;

        public CardInfoPresenter(UICardInfoView view, ITranslationService translationService)
        {
            _view = view;
            _translationService = translationService;
        }

        public void Initialize()
        {
            _view.CloseButtonClickedEvent += OnCloseButtonClicked;
        }

        public void Dispose()
        {
            _view.CloseButtonClickedEvent -= OnCloseButtonClicked;
        }

        public async void CardClicked(CardPresenter card)
        {
            CardStaticData cardData = card.CardStaticData;
            string nameCard = await _translationService.Translate($"{cardData.Id}_name", "Cards");
            string descriptionCard = await _translationService.Translate($"{cardData.Id}_description", "Cards");
            int otherStats = cardData.Type switch
            {
                CardType.Attack => cardData.Damage,
                CardType.Healing => cardData.Health,
                CardType.Ability => cardData.OtherStats
            };
            
            _view.Show(
                cardData.Background, 
                cardData.Icon, nameCard, 
                cardData.Cost.ToString(), 
                descriptionCard, 
                cardData.StatsIcon, 
                otherStats.ToString());
        }

        private void OnCloseButtonClicked()
        {
            _view.Hide();
        }
    }
}