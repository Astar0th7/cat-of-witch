using System;
using System.Linq;
using ObservableCollections;
using R3;

namespace Gameplay.Model
{
    public class PlayerProxy
    {
        private readonly PlayerData _originalData;

        public ReactiveProperty<int> MaxHealth { get; }
        public ReactiveProperty<int> MaxEnergy { get; }
        public ObservableList<string> Deck { get; }

        public PlayerProxy(PlayerData playerData)
        {
            _originalData = playerData;
            
            MaxHealth = new ReactiveProperty<int>(playerData.MaxHealth);
            MaxEnergy = new ReactiveProperty<int>(playerData.MaxEnergy);
            Deck = new ObservableList<string>(playerData.Deck);
            
            MaxHealth.Skip(1).Subscribe(value => playerData.MaxHealth = value);
            MaxEnergy.Skip(1).Subscribe(value => playerData.MaxEnergy = value);
            Deck.ObserveAdd().Skip(1).Subscribe(value => playerData.Deck.Add(value.Value));
            Deck.ObserveRemove().Subscribe(value => playerData.Deck.Remove(value.Value));
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