using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameInput
{
    public abstract int GetInputCount();
    public abstract Vector2 GetInputInPixelCoordinates(int inputId);
    public abstract Vector2 GetDeltaPosition(int inputId);
    public abstract TouchPhase GetPhase(int inputId);
    public abstract int PointerId(int index);
}
