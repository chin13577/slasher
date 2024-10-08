﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension
{

    public static Vector2 ToVector2(this Vector3 vect)
    {
        return new Vector2(vect.x, vect.y);
    }
    
    public static Vector3 ToVector3(this Vector2 vect)
    {
        return new Vector3(vect.x, vect.y);
    }
}
