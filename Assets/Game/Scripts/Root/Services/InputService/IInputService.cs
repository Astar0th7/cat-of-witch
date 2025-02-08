using System;
using UnityEngine;

namespace Root.Services.InputService
{
    public interface IInputService
    {
        event Action LeftMouseButtonDownEvent;
        event Action LeftMouseButtonUpEvent;
        event Action RightMouseButtonClickedEvent;
        event Action MenuButtonClickedEvent;
        Vector3 MousePosition { get; }
        void EnableKeyboard();
        void DisableKeyboard();
        void EnableBattle();
        void DisableBattle();
    } 
}