using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

namespace Shinnii.Controller
{
    public class MovementInput : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public DpadController controller;
        public MovementSetting setting;
        [SerializeField] private RectTransform root;
        [SerializeField] private RectTransform baseImage;
        [SerializeField] private RectTransform padImage;
        [SerializeField] private CanvasGroup canvasGroup;
        public Vector2 RawInput { get { return padImage.localPosition - baseImage.localPosition; } }
        public Vector2 Direction
        {
            get
            {
                if (RawInput.sqrMagnitude > 0.01f)
                    return RawInput.normalized;
                else
                    return Vector2.zero;
            }
        }
        public float Power { get { return RawInput.magnitude / setting.controllerRadius; } }

        private Vector2 pressPosition;
        private Vector2 currentDragPosition;
        private Vector2 origin;
        private bool isControlling;
        void Awake()
        {
            canvasGroup.alpha = setting.disableAlpha;
            origin = baseImage.anchoredPosition;
            padImage.anchoredPosition = origin;
        }

        void Update()
        {
            if (isControlling)
            {
                float currentPower = Power;
                if (currentPower > setting.minPower)
                {
                    float power = setting.isDynamicPower ? currentPower : 1;
                    controller.OnReceiveMovement(Direction, power);
                }
                else
                {
                    controller.OnReceiveMovement(Vector2.zero, 0);
                }
            }
        }
        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            canvasGroup.alpha = 1;
            pressPosition = eventData.pressPosition;
            isControlling = true;
            SetBaseImagePosition(pressPosition);
            SetPadImagePosition(pressPosition);
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            SetPadImagePosition(eventData.position);
        }

        void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
        {
            canvasGroup.alpha = setting.disableAlpha;
            isControlling = false;
            pressPosition = origin;
            SetBaseImagePosition(pressPosition);
            SetPadImagePosition(pressPosition);
            controller.OnReceiveMovement(Vector2.zero, 0);
        }

        private void SetBaseImagePosition(Vector2 position)
        {
            if ((position - origin).sqrMagnitude > setting.maxRadius * setting.maxRadius)
            {
                position = origin + (position - origin).normalized * setting.maxRadius;
            }
            baseImage.anchoredPosition = position;
        }

        private void SetPadImagePosition(Vector2 position)
        {
            if ((position - baseImage.anchoredPosition).sqrMagnitude > setting.controllerRadius * setting.controllerRadius)
            {
                position = baseImage.anchoredPosition + (position - baseImage.anchoredPosition).normalized * setting.controllerRadius;
            }
            padImage.anchoredPosition = position;
        }
    }

    [Serializable]
    public struct MovementSetting
    {
        public bool isDynamicPower;
        public float minPower;
        public float maxRadius;
        public float controllerRadius;
        [Range(0, 1f)] public float disableAlpha;
    }
}