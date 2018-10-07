using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PerspectiveDirection : MonoBehaviour
{
    public float perspectiveDegree;
    public Vector2 direction;
    public Vector3 result;
    /// <summary>
    /// Callback to draw gizmos that are pickable and always drawn.
    /// </summary>
    void OnDrawGizmos()
    {
        var calculatedDirection = direction.ConvertToPerspective2D(perspectiveDegree);
        result = new Vector3(calculatedDirection.x, calculatedDirection.y).normalized;
        Debug.DrawLine(
            transform.position,
             transform.position + result,
              Color.red, 2, false);
    }

}
public static class VectorPerspective2DExtension
{
    public static Vector3 ConvertToPerspective2D(this Vector2 vector, float perspectiveDegree)
    {
        return Quaternion.Euler(perspectiveDegree, 0, Vector2.SignedAngle(Vector2.up, vector)) * Vector3.up;
    }
}
