using UnityEngine;

namespace Gameplay.UI.View
{
    public class UIGameplayBattleView : MonoBehaviour
    {
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