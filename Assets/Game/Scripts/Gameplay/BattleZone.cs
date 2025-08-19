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
        [field: SerializeField] public Transform EnemySpawnPoint { get; private set; }
        
        private Deck _deck;
        private Battler _battler;

        private EnemyProxy _enemy;
        private PlayerBattleProxy _playerBattle;

        [Inject]
        private void Construct(Deck deck, Battler battler)
        {
            _battler = battler;
            _deck = deck;
        }

        public void ApplyCard(CardPresenter card)
        {
            if (_battler.TryUseCard(card)) 
                return;
            
            _deck.AddCard(card);
        }
    }
}