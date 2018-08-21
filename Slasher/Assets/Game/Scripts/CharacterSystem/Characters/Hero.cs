using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character, ITouchReceivable
{
    public void OnTouchBegin(Vector2 position)
    {
        if (IsReachToTargetPosition(position, transform.position.ToVector2(), 0.1f)) return;
        RotateTo(position);
        MoveTo(position);
    }

    public void OnTouchDrag(Vector2 position)
    {
        if (IsReachToTargetPosition(position, transform.position.ToVector2(), 0.1f)) return;
        RotateTo(position);
        MoveTo(position);
    }
    public void OnEnd(Vector2 position)
    {
    }

    private bool IsReachToTargetPosition(Vector2 targetPosition, Vector2 currentPosition, float minDistance)
    {
        return (targetPosition - currentPosition).SqrMagnitude() <= minDistance * minDistance;
    }
}