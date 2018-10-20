using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGizmos : MonoBehaviour
{
    public Vector3 DragPlanePosition;

    private Transform cameraTransform;
    public Transform CameraTransform
    {
        get
        {
            if (cameraTransform == null)
                cameraTransform = GetComponent<Transform>();
            return cameraTransform;
        }
    }

    void OnDrawGizmos()
    {
        Quaternion rotation = Quaternion.LookRotation(CameraTransform.forward);
        Matrix4x4 trs = Matrix4x4.TRS(transform.TransformPoint(DragPlanePosition), rotation, Vector3.one);
        Gizmos.matrix = trs;
        Color32 color = Color.blue;
        color.a = 125;
        Gizmos.color = color;
        Gizmos.DrawCube(Vector3.zero, new Vector3(5, 5, 0.0001f));
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = Color.white;
    }
}
