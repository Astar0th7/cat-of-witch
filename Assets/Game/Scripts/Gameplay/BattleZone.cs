using System;
using Gameplay.Model;
using Gameplay.Model.Battle;
using Gameplay.Model.Enemies;
using Gameplay.Presenters;
using Gameplay.View;
using UnityEngine;
using Zenject;

namespace Model
{
    public class BattleZone : MonoBehaviour
    {
        private Deck _deck;
        private Battle _battle;

        private EnemyProxy _enemy;
        private PlayerBattleProxy _playerBattle;

        [Inject]
        private void Construct(Deck deck, Battle battle)
        {
            _battle = battle;
            _deck = deck;
        }

        public void ApplyCard(CardPresenter card)
        {
            if (_battle.TryUseCard(card)) 
                return;
            
            _deck.AddCard(card);
        }
    }
}