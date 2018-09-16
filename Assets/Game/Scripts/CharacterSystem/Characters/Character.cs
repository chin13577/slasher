using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Character : MonoBehaviour, IDamageable, IUpdateable
{
    [SerializeField] protected LayerMask mask;
    public HeroData status;
    [SerializeField] protected Transform directionTransform;
    [SerializeField] protected Transform attackPoint;

    public Vector3 direction { get { return attackPoint.position - directionTransform.position; } }
    public bool CanAttack { get { return true; } }
    public new Collider2D collider;

    public virtual void TakeDamage(float damage) { }
    public virtual void TakeDamage(GameObject attacker) { }
    public virtual void OnFixedUpdate() { }
    public virtual void OnUpdate() { }
     
    protected void RotateSlerpTo(Vector2 position)
    {
        Quaternion targetRotation = RotateTo(position);
        directionTransform.transform.rotation = Quaternion.Slerp(directionTransform.transform.rotation, targetRotation, status.speed * Time.deltaTime);
    }

    private Quaternion RotateTo(Vector2 position)
    {
        return Quaternion.LookRotation(Vector3.forward, new Vector3(position.x, position.y, transform.position.z) - directionTransform.transform.position);
    }
     
    protected void MoveTo(Vector2 position)
    {
        float step = status.speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, position, step);
    }

    public Transform GetTransform()
    {
        return this.transform;
    }
}

[Serializable]
public struct HeroData
{
    public string name;
    public float attack;
    public float defence;
    public float speed;
}