using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IPushable
{
    [SerializeField] private Rigidbody2D rigid2D;
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float moveSpeed;

    public void Push(Vector2 targetDirection)
    {
        rigid2D.AddForce(targetDirection*100f);
    }

    public override void TakeDamage(float damage)
    {
        print("-hp " + damage);
    }
}
