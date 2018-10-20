using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DirectionDebuger : MonoBehaviour
{
    public float radius;
    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    { 
        Debug.DrawLine(transform.position, transform.position + (transform.up) * radius, Color.green, 2, false);
    }
}
