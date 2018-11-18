using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    [SerializeField]
    private float smoothTime;
    [SerializeField]
    private Transform target;
    private Vector2 position;
    private Vector3 velocityPosition;

    public void Initialize(Transform target)
    {
        this.target = target;
    }

    void LateUpdate()
    {
        if (target != null)
            position = target.position;
        else
            position = transform.position;
        transform.position = Vector3.SmoothDamp(transform.position, position, ref velocityPosition, smoothTime * Time.unscaledDeltaTime);

    }
}
