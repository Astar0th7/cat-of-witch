using UnityEngine;

namespace Gameplay.UI.View
{
    public class UITimingEffectsView : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        
        public Transform Container => _container;
    }
}