using System;
using UnityEngine;

namespace Gameplay.View
{
    public class Deck : MonoBehaviour
    {
        public Vector3 Position => transform.position;

        private void Start()
        {
            Camera main = Camera.main;
            Vector3 screenBottomLeft = new(0, 0, main.nearClipPlane);
            Vector3 bottomLeft = main.ScreenToWorldPoint(screenBottomLeft);
            bottomLeft.z = 0;
            transform.position = bottomLeft;
            
            Refresh();
        }

        public void RevertCard(CardView freeCardView)
        {
            freeCardView.transform.SetParent(transform);

            Refresh();
        }

        public void Refresh()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                CardView currentCardView = transform.GetChild(i).GetComponent<CardView>();
                
                Vector3 targetPosition = Position;
                
                if (i == 0)
                {
                    targetPosition.x += currentCardView.Width / 2 + 0.1f;
                }
                else
                {
                    CardView previousCardView = transform.GetChild(i - 1).GetComponent<CardView>();
                    targetPosition.x = previousCardView.TargetPosition.x + (currentCardView.Width + 0.1f);
                }
                
                targetPosition.y += currentCardView.Height / 2 + 0.5f;
                targetPosition.z = 0;
                
                currentCardView.MoveToTarget(targetPosition);
            }
        }
    }
}