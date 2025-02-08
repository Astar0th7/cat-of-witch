using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Root.Services.InputService
{
    public class DesktopInputService : IInputService
    {
        public event Action LeftMouseButtonDownEvent;
        public event Action LeftMouseButtonUpEvent;
        public event Action RightMouseButtonClickedEvent;
        public event Action MenuButtonClickedEvent;

        private readonly DesktopInputMap _inputMap;

        public Vector3 MousePosition => Input.mousePosition;
        
        public DesktopInputService()
        {
            _inputMap = new DesktopInputMap();
            
            _inputMap.Mouse.Drag.started += OnLeftMouseButtonDown;
            _inputMap.Mouse.Drag.canceled += OnLeftMouseButtonUp;
            _inputMap.Mouse.Info.performed += OnRightMouseButtonClicked;
            _inputMap.Keyboard.Menu.performed += OnMenuButtonClicked;
        }

        public void EnableKeyboard()
        {
            _inputMap.Keyboard.Enable();
        }

        public void DisableKeyboard()
        {
            _inputMap.Keyboard.Disable();
        }

        public void EnableBattle()
        {
            _inputMap.Mouse.Enable();
        }
        
        public void DisableBattle()
        {
            _inputMap.Mouse.Disable();
        }

        private void OnLeftMouseButtonDown(InputAction.CallbackContext context) => 
            LeftMouseButtonDownEvent?.Invoke();

        private void OnLeftMouseButtonUp(InputAction.CallbackContext context) => 
            LeftMouseButtonUpEvent?.Invoke();

        private void OnRightMouseButtonClicked(InputAction.CallbackContext context) => 
            RightMouseButtonClickedEvent?.Invoke();

        private void OnMenuButtonClicked(InputAction.CallbackContext context) => 
            MenuButtonClickedEvent?.Invoke();
    }
}