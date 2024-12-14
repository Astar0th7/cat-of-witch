using System;
using Gameplay.Model;
using Gameplay.Model.Enemies;
using Gameplay.View;
using R3;
using UnityEngine;
using Zenject;

namespace Gameplay.Presenters
{
    public class CardPresenter : IInitializable, IDisposable
    {
        private readonly CardView _view;
        private readonly CardModel _model;
        
        private IDisposable _disposable;

        public Vector3 TargetPosition => _model.TargetPosition.Value;
        public Vector3 Position => _view.transform.position;
        public float Width => _view.Width;
        public float Height => _view.Height;

        public event Action<CardPresenter> CardSelectEvent;
        public event Action<CardPresenter> CardApplyEvent; 

        public CardPresenter(CardView view, CardModel model)
        {
            _view = view;
            _model = model;
        }

        public void Initialize()
        {
            _view.CardSelectEvent += OnCardSelect;
            _disposable = _model.TargetPosition.Subscribe(value => _view.SetTarget(value));
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }

        public void SetTargetPosition(Vector3 targetPosition)
        {
            if (Vector3.SqrMagnitude(targetPosition - _model.TargetPosition.Value) < 0.01f)
                return;
            
            _model.TargetPosition.Value = targetPosition;
        }

        public void SetOrderInLayer(int order)
        {
            _view.SetOrderInLayer(order);
        }

        public bool TryApply(PlayerBattleProxy player, EnemyProxy enemy)
        {
            if (_model.TryApply(player, enemy) == false)
                return false;
            
            Dispose();
            _view.Apply();
            CardApplyEvent?.Invoke(this);
            return true;
        }

        private void OnCardSelect()
        {
            CardSelectEvent?.Invoke(this);
        }
    }
}