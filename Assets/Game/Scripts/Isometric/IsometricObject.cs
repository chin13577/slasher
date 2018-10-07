using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricObject : MonoBehaviour
{
    public Vector3 position;
    public SpriteRenderer renderer;

#if UNITY_EDITOR
    /// <summary>
    /// Callback to draw gizmos only if the object is selected.
    /// </summary>
    void OnDrawGizmosSelected()
    {
        transform.position = Isometric.twoDToIso(position);
        Isometric.UpdateLayerInZAxis(transform, position);
    }
#endif
}
