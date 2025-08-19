using UnityEngine;

namespace Gameplay.UI.View
{
    public class Location : MonoBehaviour
    {
        [field: SerializeField] public Transform EnemySpawnPoint { get; private set; }
        
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}