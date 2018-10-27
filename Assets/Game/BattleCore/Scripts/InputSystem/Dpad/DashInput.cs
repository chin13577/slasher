using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Shinnii.Controller
{
    public class DashInput : MonoBehaviour, IPointerDownHandler
    {
        public DpadController controller;
        [SerializeField] Image image;

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            controller.OnReceiveDashEvent();
        }

    }
}