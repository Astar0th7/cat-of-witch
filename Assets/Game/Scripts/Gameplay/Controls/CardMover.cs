using Gameplay.Presenters;
using Model;
using Gameplay.View;
using Root.Services.InputService;
using UnityEngine;
using Zenject;

namespace Gameplay.Controls
{
    public class CardMover : MonoBehaviour
    {
        private Deck _deck;
        private BattleZone _battleZone;
        private IInputService _inputService;
        
        private Camera _camera;
        private CardPresenter _card;
        private Vector3 _mousePosition;

        private readonly RaycastHit2D[] _hits = new RaycastHit2D[1];

        [Inject]
        private void Construct(Deck deck, BattleZone battleZone, IInputService inputService)
        {
            _deck = deck;
            _battleZone = battleZone;
            _inputService = inputService;
        }

        private void Start()
        {
            _camera = Camera.main;

            _inputService.Enable();
            _inputService.LeftMouseButtonDownEvent += OnMouseButtonDown;
            _inputService.LeftMouseButtonUpEvent += OnMouseButtonUp;
            
            _deck.CardSelectEvent += OnCardSelectEvent;
        }

        private void OnDestroy()
        {
            _inputService.LeftMouseButtonDownEvent -= OnMouseButtonDown;
            _inputService.LeftMouseButtonUpEvent -= OnMouseButtonUp;
            
            _deck.CardSelectEvent -= OnCardSelectEvent;
        }

        private void Update()
        {
            if (_card == null)
                return;

            Vector3 currentMousePosition = _inputService.MousePosition;
            if (Vector3.SqrMagnitude(currentMousePosition - _mousePosition) < 0.1f)
                return;
            
            _mousePosition = currentMousePosition;
            Vector3 targetPosition = _camera.ScreenToWorldPoint(_mousePosition);
            targetPosition.z = 0f;
            _card.SetTargetPosition(targetPosition);
            _card.SetTargetRotation(Quaternion.identity);
        }

        private void OnMouseButtonDown()
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(_inputService.MousePosition);
            Physics2D.RaycastNonAlloc(mousePosition, Vector2.zero, _hits, Mathf.Infinity);

            Collider2D collider = _hits[0].collider;
            if (collider != null && collider.TryGetComponent(out CardView card)) 
                card.Select();
        }

        private void OnMouseButtonUp()
        {
            if (_card == null)
                return;
            
            if (_card.Position.y >= _battleZone.transform.position.y)
                _battleZone.ApplyCard(_card);
            else
                _deck.AddCard(_card);
            
            _card = null;
            _hits[0] = new RaycastHit2D();
        }

        private void OnCardSelectEvent(CardPresenter card)
        {
            _card = card;
            _card.SetOrderInLayer(10);
        }
    }
}