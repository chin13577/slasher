using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shinnii.Controller;

public abstract class Character : MonoBehaviour, IReceiveMovement
{
    public Status status = new Status();
    public Rigidbody2D rigid;

    void Awake()
    {

    }

    public virtual void OnReceiveMovement(Vector2 direction)
    {
        Move(direction);
    }

    private void Move(Vector2 direction)
    {
        Vector3 newPos = this.transform.position + new Vector3(direction.x, direction.y, 0) * status.speed * Time.deltaTime;
        rigid.MovePosition(newPos);

    }
}