using Cysharp.Threading.Tasks;
using Gameplay.Model;
using Gameplay.Model.Enemies;
using Gameplay.Presenters;
using UnityEngine;

namespace Gameplay.Services
{
    public interface IBattleFactory
    {
        UniTask<CardPresenter> CreateCard(CardStaticData data);
        EnemyProxy CreateEnemy(EnemyStaticData enemyStaticData, Transform point);
        PlayerBattleProxy CreatePlayer();
    }
}