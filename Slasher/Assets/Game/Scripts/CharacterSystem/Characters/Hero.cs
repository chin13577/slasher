using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character, ITouchReceivable
{
    public void OnTouchBegin(Vector2 position)
    {
        MoveTo(position);
    }
    public void OnTouchDrag(Vector2 position)
    {
        MoveTo(position);
    }
    public void OnEnd(Vector2 position)
    {
    }

    private void MoveTo(Vector2 position)
    {
        float step = status.speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, position, step);
    }

}
