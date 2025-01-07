using Gameplay.View;
using Root.Services.InputService;
using UnityEngine;
using Zenject;

namespace Gameplay.Controls
{
    public class GameplayInput : MonoBehaviour
    {
        private IInputService _inputService;
        private Camera _camera;

        private readonly RaycastHit2D[] _hits = new RaycastHit2D[1];

        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void Start()
        {
            _camera = Camera.main;
            _inputService.RightMouseButtonClickedEvent += OnRightMouseButtonClicked;
        }

        private void OnDestroy()
        {
            _inputService.RightMouseButtonClickedEvent -= OnRightMouseButtonClicked;
        }

        private void OnRightMouseButtonClicked()
        {
            Vector3 mousePosition = _camera.ScreenToWorldPoint(_inputService.MousePosition);
            Physics2D.RaycastNonAlloc(mousePosition, Vector2.zero, _hits, Mathf.Infinity);

            Collider2D collider = _hits[0].collider;
            if (collider != null && collider.TryGetComponent(out CardView card))
                card.OnClicked();
            
            _hits[0] = new RaycastHit2D();
        }
    }
}