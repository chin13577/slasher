using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IPushable
{
    [SerializeField] private Rigidbody2D rigid2D;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveSpeed;

    public override void OnFixedUpdate()
    {
        RotateSlerpTo(Vector2.zero);
        MoveTo(Vector2.zero);
        //rigid2D.AddForce(direction * moveSpeed);
        //print(rigid2D.velocity);
    }
    public void Push(Vector2 targetDirection)
    {
        rigid2D.AddRelativeForce(targetDirection);
    }

    public override void TakeDamage(float damage)
    {
        print("-hp " + damage);
    }
}
