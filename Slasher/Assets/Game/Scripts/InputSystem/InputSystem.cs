using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private GameInput input;

    private void Awake()
    {
#if UNITY_EDITOR
        input = new DebugInput();
#elif UNITY_ANDROID
        input = new TouchInput();
#endif
        Input.multiTouchEnabled = false;
    }
    
    private void Update()
    {
        for (int i = 0; i < input.GetInputCount(); i++)
        {
            TouchPhase phase = input.GetPhase(i);
            print(phase);
            if (phase == TouchPhase.Began)
            {
                if (EventSystem.current.IsPointerOverGameObject(input.PointerId(i))) { return; }
                
            }
            else if (phase == TouchPhase.Moved)
            {
            }
            else if (phase == TouchPhase.Ended)
            {
            }
        }
    }

}