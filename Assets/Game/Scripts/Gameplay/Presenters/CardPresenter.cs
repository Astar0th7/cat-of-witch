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
        
        private CompositeDisposable _subscriptions;

        public CardStaticData CardStaticData => _model.OriginalData;
        public Vector3 TargetPosition => _model.TargetPosition.Value;
        public Vector3 Position => _view.transform.position;
        public float Width => _view.Width;
        public float Height => _view.Height;
        
        public event Action<CardPresenter> CardSelectEvent;
        public event Action<CardPresenter> CardApplyEvent; 
        public event Action<CardPresenter> CardClickedEvent;

        public CardPresenter(CardView view, CardModel model)
        {
            _view = view;
            _model = model;
        }

        public void Initialize()
        {
            _view.CardSelectEvent += OnCardSelect;
            _view.CardClickedEvent += OnCardClicked;
            _subscriptions = new CompositeDisposable();
            _subscriptions.Add(_model.TargetPosition.Subscribe(value => _view.SetTargetPosition(value)));
            _subscriptions.Add(_model.TargetRotation.Subscribe(value => _view.SetTargetRotation(value)));
        }

        public void Dispose()
        {
            _subscriptions?.Dispose();
        }

        public void SetTargetPosition(Vector3 targetPosition)
        {
            if (Vector3.SqrMagnitude(targetPosition - _model.TargetPosition.Value) < 0.01f)
                return;
            
            _model.TargetPosition.Value = targetPosition;
        }

        public void SetTargetRotation(Quaternion targetRotation)
        {
            _model.TargetRotation.Value = targetRotation;
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

        public void FastRemove()
        {
            _view.FastRemove();
        }

        public void Remove()
        {
            _view.Remove();
        }

        private void OnCardSelect()
        {
            CardSelectEvent?.Invoke(this);
        }

        private void OnCardClicked()
        {
            CardClickedEvent?.Invoke(this);
        }
    }
}