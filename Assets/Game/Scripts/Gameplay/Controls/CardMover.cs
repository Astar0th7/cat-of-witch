using Gameplay.View;
using Root.Services.InputService;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay.Controls
{
    public class CardMover : MonoBehaviour
    {
        private IInputService _inputService;
        private Deck _deck;
        private Camera _camera;
        private CardView _cardView;
        private BattleZone _battleZone;

        private readonly RaycastHit2D[] _hits = new RaycastHit2D[1];

        [Inject]
        private void Construct(IInputService inputService, Deck deck, BattleZone battleZone)
        {
            _inputService = inputService;
            _deck = deck;
            _battleZone = battleZone;
        }

        private void Start()
        {
            _camera = Camera.main;

            _inputService.Enable();
            _inputService.MouseButtonDownEvent += OnMouseButtonDown;
            _inputService.MouseButtonUpEvent += OnMouseButtonUp;
        }

        private void OnDestroy()
        {
            _inputService.MouseButtonDownEvent -= OnMouseButtonDown;
            _inputService.MouseButtonUpEvent -= OnMouseButtonUp;
        }

        private void Update()
        {
            if (_hits[0].collider == null)
                return;
            
            Vector3 targetPosition = _camera.ScreenToWorldPoint(_inputService.MousePosition);
            targetPosition.z = 0f;
            _cardView.MoveToTarget(targetPosition);
        }

        private void OnMouseButtonDown()
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(_inputService.MousePosition);
            Physics2D.RaycastNonAlloc(mousePosition, Vector2.zero, _hits, Mathf.Infinity);

            Collider2D collider = _hits[0].collider;
            if (collider != null && collider.TryGetComponent(out CardView card))
            {
                _cardView = card;
                _cardView.transform.SetParent(null);
                _deck.Refresh();
            }
        }

        private void OnMouseButtonUp()
        {
            if (_cardView == null)
                return;
            
            if (_cardView.transform.position.y >= _battleZone.transform.position.y)
                _battleZone.ApplyCard(_cardView);
            else
                _deck.RevertCard(_cardView);
            
            _hits[0] = new RaycastHit2D();
        }
    }
}