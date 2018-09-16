using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputSystem : MonoBehaviour
{
    [SerializeField] Camera usageCamera;
    protected Camera cam
    {
        get
        {
            if (usageCamera == null)
                return Camera.main;
            else
                return usageCamera;
        }
    }

    private GameManager manager;
    private ITouchReceivable receiver;
    private GameInput input;

    public void Initialize(GameManager manager)
    {
        this.manager = manager;
#if UNITY_EDITOR
        input = new DebugInput();
#elif UNITY_ANDROID
        input = new TouchInput();
#endif
        Input.multiTouchEnabled = false;
    }

    public void SetReceiver(ITouchReceivable receiver)
    {
        this.receiver = receiver;
    }

    private void Update()
    {
        for (int i = 0; i < input.GetInputCount(); i++)
        {
            TouchPhase phase = input.GetPhase(i); 
            if (phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(input.PointerId(i))) { return; }
                if (receiver != null)
                    receiver.OnTouchBegin(cam.ScreenToWorldPoint(input.GetInputInPixelCoordinates(i)));

            }
            else if (phase == TouchPhase.Moved || phase == TouchPhase.Stationary)
            {
                if (receiver != null)
                    receiver.OnTouchDrag(cam.ScreenToWorldPoint(input.GetInputInPixelCoordinates(i)));
            }
            else if (phase == TouchPhase.Ended)
            {
                if (receiver != null)
                    receiver.OnEnd(cam.ScreenToWorldPoint(input.GetInputInPixelCoordinates(i)));
            }
        }
    }

}