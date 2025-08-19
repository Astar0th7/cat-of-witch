using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.UI.View
{
    public class UISliderView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private Slider _slider;

        private Tween _slideValueTween;
        
        public void SetValue(int current, int max)
        {
            _slideValueTween?.Kill(true);

            if (_amountText != null)
                _amountText.text = current.ToString();
            
            float target = (float)current / max;
            _slideValueTween = _slider.DOValue(target, 0.1f).SetEase(Ease.InOutSine);
        }
    }
}