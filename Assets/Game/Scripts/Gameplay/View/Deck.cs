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

        public void RevertCard(Card freeCard)
        {
            freeCard.transform.SetParent(transform);

            Refresh();
        }

        public void Refresh()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Card currentCard = transform.GetChild(i).GetComponent<Card>();
                
                Vector3 targetPosition = Position;
                
                if (i == 0)
                {
                    targetPosition.x += currentCard.Width / 2 + 0.1f;
                }
                else
                {
                    Card previousCard = transform.GetChild(i - 1).GetComponent<Card>();
                    targetPosition.x = previousCard.TargetPosition.x + (currentCard.Width + 0.1f);
                }
                
                targetPosition.y += currentCard.Height / 2 + 0.5f;
                targetPosition.z = 0;
                
                Debug.Log(targetPosition);
                currentCard.MoveToTarget(targetPosition);
            }
        }
    }
}