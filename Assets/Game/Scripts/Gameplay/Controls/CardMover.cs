using System;
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
        private Card _card;

        private readonly RaycastHit2D[] _hits = new RaycastHit2D[1];

        [Inject]
        private void Construct(IInputService inputService, Deck deck)
        {
            _inputService = inputService;
            _deck = deck;
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
            _card.MoveToTarget(targetPosition);
        }

        private void OnMouseButtonDown()
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(_inputService.MousePosition);
            Physics2D.RaycastNonAlloc(mousePosition, Vector2.zero, _hits, Mathf.Infinity);

            Collider2D collider = _hits[0].collider;
            if (collider != null && collider.TryGetComponent(out Card card))
            {
                _card = card;
                _card.transform.SetParent(null);
                _deck.Refresh();
            }
        }

        private void OnMouseButtonUp()
        {
            _deck.RevertCard(_card);
            _hits[0] = new RaycastHit2D();
        }
    }
}