using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shinnii.Controller
{
    public class AttackInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public DpadController controller;
        [SerializeField] Image image;
        public Vector2 Direction { get; set; }
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            Direction = Vector2.zero;
            controller.OnReceiveAttackEnter();
            image.color = new Color(1, 1, 1, 0.8f);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            Direction = (eventData.position - eventData.pressPosition).normalized;
            controller.OnReceiveAttackDrag(Direction);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            Direction = Vector2.zero;
            controller.OnReceiveAttackUp();
            image.color = new Color(1, 1, 1, 1);
        }

    }
}
