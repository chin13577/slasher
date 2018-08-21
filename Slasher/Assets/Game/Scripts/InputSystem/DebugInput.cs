using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugInput : GameInput
{
    Vector2 oldPos;
    Vector2 currentPos { get { return Input.mousePosition; } }
    Vector2 deltaPos;

    public override int GetInputCount()
    {
        if (Input.GetMouseButton(0) || Input.GetMouseButtonUp(0))
            return 1;
        else
            return 0;
    }

    public override Vector2 GetInputInPixelCoordinates(int inputId)
    {
        return Input.mousePosition;
    }
    public override Vector2 GetDeltaPosition(int inputId)
    {
        return deltaPos;
    }


    public override TouchPhase GetPhase(int inputId)
    {
        if (Input.GetMouseButtonDown(inputId))
        {
            deltaPos = Vector2.zero;
            oldPos = currentPos;
            return TouchPhase.Began;
        }
        else if (Input.GetMouseButton(inputId))
        {
            float minDist = 0.01f;
            deltaPos = currentPos - oldPos;
            if ((currentPos - oldPos).sqrMagnitude < minDist * minDist)
            {
                return TouchPhase.Stationary;
            }
            else
            {
                oldPos = currentPos;
                return TouchPhase.Moved;
            }
        }
        else
            return TouchPhase.Ended;
    }

    public override int PointerId(int index)
    {
        return -1;
    }
}
