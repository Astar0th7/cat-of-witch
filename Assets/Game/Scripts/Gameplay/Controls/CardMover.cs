using Gameplay.Presenters;
using Gameplay.View;
using Root.Services.InputService;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay.Controls
{
    public class CardMover : MonoBehaviour
    {
        private Deck _deck;
        private BattleZone _battleZone;
        private IInputService _inputService;
        
        private Camera _camera;
        private CardPresenter _card;

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
            _inputService.MouseButtonDownEvent += OnMouseButtonDown;
            _inputService.MouseButtonUpEvent += OnMouseButtonUp;
            
            _deck.CardSelectEvent += OnCardSelectEvent;
        }

        private void OnDestroy()
        {
            _inputService.MouseButtonDownEvent -= OnMouseButtonDown;
            _inputService.MouseButtonUpEvent -= OnMouseButtonUp;
            
            _deck.CardSelectEvent -= OnCardSelectEvent;
        }

        private void Update()
        {
            if (_card == null)
                return;
            
            Vector3 targetPosition = _camera.ScreenToWorldPoint(_inputService.MousePosition);
            targetPosition.z = 0f;
            _card.SetTargetPosition(targetPosition);
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
        }
    }
}