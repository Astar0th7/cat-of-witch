using System;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Gameplay.Model;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Gameplay.View
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider2D;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _costText;
        [SerializeField] private TMP_Text _otherStatsText;
        [SerializeField] private SpriteRenderer _backgroundSprite;
        [SerializeField] private SpriteRenderer _iconSprite;
        [SerializeField] private SpriteRenderer _otherStatsSprite;

        private Vector3 _targetPosition;

        public float Width => _collider2D.size.x;
        public float Height => _collider2D.size.y;

        public event Action CardSelectEvent;

        private void FixedUpdate()
        {
            if (Vector3.SqrMagnitude(transform.position - _targetPosition) < 0.0001f)
                return;
            
            transform.position = Vector3.Lerp(transform.position, _targetPosition, Time.fixedDeltaTime * 10f);
        }

        public void SetBaseInfo(Sprite backgroundSprite, Sprite iconSprite, string nameCard, string description, int costAmount)
        {
            _iconSprite.sprite = iconSprite;
            _nameText.text = nameCard;
            _descriptionText.text = description;
            _costText.text = costAmount.ToString();
            _backgroundSprite.sprite = backgroundSprite;
        }

        public void SetOtherStats(int otherStats, Sprite otherStatsSprite)
        {
            _otherStatsText.text = otherStats <= 0 ? string.Empty : otherStats.ToString();

            if (otherStatsSprite == null)
            {
                _otherStatsSprite.sprite = otherStatsSprite;
                _otherStatsSprite.color = Color.clear;
            }
            else
            {
                _otherStatsSprite.sprite = otherStatsSprite;
            }
        }

        public void SetTarget(Vector3 targetPosition)
        {
            _targetPosition = targetPosition;
        }

        public void Apply()
        {
            _collider2D.enabled = false;
            transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack).OnComplete(() => Destroy(gameObject));
        }

        public void Select()
        {
            CardSelectEvent?.Invoke();
        }
    }
}