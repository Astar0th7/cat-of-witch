using System;
using ObservableCollections;
using R3;

namespace Root.Model
{
    public class PlayerProxy
    {
        private readonly PlayerData _originalData;

        public ReactiveProperty<int> MaxHealth { get; }
        public ReactiveProperty<int> MaxEnergy { get; }
        public ObservableList<string> Deck { get; }
        
        public PlayerData OriginalData => _originalData;

        public PlayerProxy(PlayerData playerData)
        {
            _originalData = playerData;
            
            MaxHealth = new ReactiveProperty<int>(_originalData.MaxHealth);
            MaxEnergy = new ReactiveProperty<int>(_originalData.MaxEnergy);
            Deck = new ObservableList<string>(_originalData.Deck);
            
            MaxHealth.Skip(1).Subscribe(value => _originalData.MaxHealth = value);
            MaxEnergy.Skip(1).Subscribe(value => _originalData.MaxEnergy = value);
            Deck.ObserveAdd().Subscribe(value => _originalData.Deck.Add(value.Value));
            Deck.ObserveRemove().Subscribe(value => _originalData.Deck.Remove(value.Value));
        }

        public void AddCardToDeck(string cardId)
        {
            if (Deck.Contains(cardId))
                throw new AggregateException($"Карта {cardId} уже добавлена в колоду");
            
            Deck.Add(cardId);
        }

        public void RemoveCardFromDeck(string cardId)
        {
            if (Deck.Contains(cardId) == false)
                throw new AggregateException($"Карта {cardId} отсутствует в колоде");

            Deck.Remove(cardId);
        }
    }
}