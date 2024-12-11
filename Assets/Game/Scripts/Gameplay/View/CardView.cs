using System.Collections;
using Gameplay.Model;
using TMPro;
using UnityEngine;

namespace Gameplay.View
{
    public class CardView : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider2D;
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private TMP_Text _descriptionText;
        [SerializeField] private TMP_Text _costText;
        [SerializeField] private TMP_Text _otherStatsText;
        [SerializeField] private SpriteRenderer _iconSprite;
        [SerializeField] private SpriteRenderer _otherStatsSprite;

        public float Width => _collider2D.size.x;
        public float Height => _collider2D.size.y;

        public Vector3 TargetPosition { get; private set; }
        public CardBase Card { get; private set; }

        private void FixedUpdate()
        {
            if (Vector3.SqrMagnitude(TargetPosition - transform.position) < 0.001f)
                return;
            
            transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.fixedDeltaTime * 10f);
        }

        public void SetBaseInfo(CardBase card, Sprite iconSprite, string nameCard, string description, int costAmount)
        {
            Card = card;
            _iconSprite.sprite = iconSprite;
            _nameText.text = nameCard;
            _descriptionText.text = description;
            _costText.text = costAmount.ToString();
        }

        public void SetOtherStats(int otherStats = 0, Sprite otherStatsSprite = null)
        {
            _otherStatsText.text = otherStats <= 0 ? string.Empty : otherStats.ToString();

            if (otherStatsSprite != null)
            {
                _otherStatsSprite.sprite = otherStatsSprite;
                _otherStatsSprite.color = Color.clear;
            }
            else
            {
                _otherStatsSprite.sprite = otherStatsSprite;
            }
        }

        public void MoveToTarget(Vector3 targetPosition)
        {
            TargetPosition = targetPosition;
        }

        public void Apply()
        {
            _collider2D.enabled = false;
            StartCoroutine(Remove());
        }

        private IEnumerator Remove()
        {
            while (transform.localScale.x > 0.1f)
            {
                transform.localScale -= Vector3.one * (5f * Time.deltaTime);
                yield return null;
            }
            
            Destroy(gameObject);
        }
    }
}