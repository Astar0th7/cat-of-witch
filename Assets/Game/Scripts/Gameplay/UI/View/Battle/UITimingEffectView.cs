using TMPro;
using UnityEngine;

namespace Gameplay.UI.View
{
    public class UITimingEffectView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeText;

        public void SetTime(int time)
        {
            _timeText.text = time.ToString();
        }

        public void Remove()
        {
            Destroy(gameObject);
        }
    }
}