using System;
using UnityEngine;

namespace Root.UI.View
{
    public class UILoadingScreenView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        public void Show()
        {
            _canvas.enabled = true;
        }

        public void Hide()
        {
            _canvas.enabled = false;
        }
    }
}