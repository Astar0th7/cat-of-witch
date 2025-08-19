using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Root.Model;
using Root.Services.Translation;
using Gameplay.Model;
using Gameplay.Model.Enemies;
using Gameplay.Presenters;
using Gameplay.View;
using Model.Effects;
using Root.Services;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class BattleFactory : IBattleFactory
    {
        private readonly PlayerProxy _player;
        private readonly IInstantiator _instantiator;
        private readonly ITranslationService _translationService;
        private readonly IStaticDataService _staticDataService;

        public BattleFactory(
            PlayerProxy player, 
            IInstantiator instantiator,
            ITranslationService translationService, 
            IStaticDataService staticDataService)
        {
            _player = player;
            _instantiator = instantiator;
            _translationService = translationService;
            _staticDataService = staticDataService;
        }

        public async UniTask<CardPresenter> CreateCard(CardStaticData data)
        {
            CardView cardView = Instantiate(_staticDataService.Card, Vector3.zero, Quaternion.identity);
                
            CardModel cardModel = data.Type switch
            {
                CardType.Attack => new CardAttack(data),
                CardType.Ability => throw new ArgumentOutOfRangeException(),
                CardType.Healing => new CardHealing(data),
                CardType.Effect => CreateCardEffect(data)
            };

            CardPresenter card = new(cardView, cardModel);
            card.Initialize();

            string nameCard = await _translationService.Translate($"{data.Id}_name", "Cards");
            string descriptionCard = await _translationService.Translate($"{data.Id}_description", "Cards");
                
            cardView.SetBaseInfo(data.Background, data.Icon, nameCard, descriptionCard, data.Cost);
            cardView.SetOtherStats(cardModel.Stats, data.StatsIcon);

            return card;
        }

        public EnemyProxy CreateEnemy(EnemyStaticData enemyStaticData, Transform point)
        {
            EnemyView enemyView = Instantiate(enemyStaticData.EnemyPrefab, point.position, Quaternion.identity);
            
            EnemyData data = new()
            {
                Id = enemyStaticData.Id,
                EnemyView = enemyView,
                CurrentHealth = enemyStaticData.MaxHealth,
                MaxHealth = enemyStaticData.MaxHealth,
                Damage = enemyStaticData.Damage,
                Heal = enemyStaticData.Heal
            };

            List<Ability> abilities = new();
            EnemyProxy enemy = new(data, abilities);

            foreach (AbilityStaticData abilityData in enemyStaticData.Abilities) 
                abilities.Add(AbilityCreator.Creator[abilityData.TypeId](abilityData));
            
            return enemy;
        }

        public PlayerBattleProxy CreatePlayer()
        {
            PlayerBattleData playerBattleData = new()
            {
                CurrentHealth = _player.MaxHealth.Value,
                MaxHealth = _player.MaxHealth.Value,
                CurrentEnergy = _player.MaxEnergy.Value,
                MaxEnergy = _player.MaxEnergy.Value,
            };

            return new PlayerBattleProxy(playerBattleData);
        }

        private CardModel CreateCardEffect(CardStaticData cardData)
        {
            BaseEffect effect = EffectCreator.Creator[cardData.EffectData.Id](cardData.EffectData);
            return new CardTimingEffect(cardData, effect);
        }

        private T Instantiate<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : MonoBehaviour
        {
            return _instantiator.InstantiatePrefabForComponent<T>(prefab, position, rotation, parent);
        }
    }
}