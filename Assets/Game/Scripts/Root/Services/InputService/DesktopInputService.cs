using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Root.Services.InputService
{
    public class DesktopInputService : IInputService
    {
        public event Action MouseButtonDownEvent;
        public event Action MouseButtonUpEvent;

        private readonly DesktopInputMap _inputMap;

        public Vector3 MousePosition => Input.mousePosition;
        
        public DesktopInputService()
        {
            _inputMap = new DesktopInputMap();
            
            _inputMap.Mouse.Drag.started += OnMouseButtonDown;
            _inputMap.Mouse.Drag.canceled += OnMouseButtonUp;
        }

        public void Enable()
        {
            _inputMap.Enable();
        }

        public void Disable()
        {
            _inputMap.Disable();
        }

        private void OnMouseButtonDown(InputAction.CallbackContext context)
        {
            MouseButtonDownEvent?.Invoke();
        }

        private void OnMouseButtonUp(InputAction.CallbackContext context)
        {
            MouseButtonUpEvent?.Invoke();
        }
    }
}