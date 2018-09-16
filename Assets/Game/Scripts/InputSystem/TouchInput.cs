using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInput : GameInput
{
    public override int GetInputCount()
    {
        return Input.touchCount;
    }
    public override Vector2 GetDeltaPosition(int inputId)
    {
        return Input.GetTouch(inputId).deltaPosition;
    }

    public override Vector2 GetInputInPixelCoordinates(int inputId)
    {
        return Input.GetTouch(inputId).position;
    }

    public override TouchPhase GetPhase(int inputId)
    {
        TouchPhase phase = Input.GetTouch(inputId).phase;
        if (phase == TouchPhase.Moved)
        {
            float minDist = 1f;
            Vector2 deltaPos = Input.GetTouch(inputId).deltaPosition;
            if (deltaPos.sqrMagnitude < minDist * minDist)
            {
                return TouchPhase.Stationary;
            }
            else
                return TouchPhase.Moved;
        }
        else
            return phase;
    }

    public override int PointerId(int index)
    {
        return Input.GetTouch(index).fingerId;
    }
}
