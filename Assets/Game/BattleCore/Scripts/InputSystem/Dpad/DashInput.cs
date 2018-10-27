using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shinnii.Controller
{
    public class DashInput : MonoBehaviour, IPointerDownHandler,IPointerUpHandler
    {
        public DpadController controller;
        [SerializeField] Image image;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                controller.OnReceiveDashEvent();
            }
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            controller.OnReceiveDashEvent();
            image.color = new Color(1, 1, 1, 0.8f);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            image.color = new Color(1, 1, 1, 1);
        }
    }
}