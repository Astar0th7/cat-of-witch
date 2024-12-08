using System;
using UnityEngine;

namespace Root.Services.InputService
{
    public interface IInputService
    {
        event Action MouseButtonDownEvent;
        event Action MouseButtonUpEvent;
        Vector3 MousePosition { get; }
        void Enable();
        void Disable();
    }
}