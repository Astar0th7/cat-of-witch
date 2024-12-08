using System;
using UnityEngine;

namespace Gameplay.View
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private BoxCollider2D _collider2D;

        public float Width => _collider2D.size.x;
        public float Height => _collider2D.size.y;

        public Vector3 TargetPosition { get; private set; }

        private void FixedUpdate()
        {
            if (Vector3.SqrMagnitude(TargetPosition - transform.position) < 0.0001f)
                return;
            
            transform.position = Vector3.Lerp(transform.position, TargetPosition, Time.fixedDeltaTime * 10f);
        }

        public void MoveToTarget(Vector3 targetPosition)
        {
            TargetPosition = targetPosition;
        }
    }
}