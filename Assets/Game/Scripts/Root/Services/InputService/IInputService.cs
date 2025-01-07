using System;
using UnityEngine;

namespace Root.Services.InputService
{
    public interface IInputService
    {
        event Action LeftMouseButtonDownEvent;
        event Action LeftMouseButtonUpEvent;
        Vector3 MousePosition { get; }
        void Enable();
        void Disable();
        event Action RightMouseButtonClickedEvent;
    } 
}