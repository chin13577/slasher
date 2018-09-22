using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character, ITouchReceivable
{
    public Weapon weapon;

    private Vector2 oldPos;
    private Vector2 currentPos { get { return transform.position; } }

    private Collider2D[] overlapCollisions = new Collider2D[3];
    private RaycastHit2D[] raycastHits = new RaycastHit2D[3];

    #region ReceiveInput

    public void OnTouchBegin(Vector2 position)
    {
        if (IsReachToTargetPosition(position, transform.position.ToVector2(), 0.1f)) return;
        RotateSlerpTo(position);
        MoveTo(position);
    }

    public void OnTouchDrag(Vector2 position)
    {
        if (IsReachToTargetPosition(position, transform.position.ToVector2(), 0.1f)) return;
        RotateSlerpTo(position);
        MoveTo(position);
        OnUpdate();
    }
    public void OnEnd(Vector2 position)
    {
    }

    #endregion

    private bool IsReachToTargetPosition(Vector2 targetPosition, Vector2 currentPosition, float minDistance)
    {
        return (targetPosition - currentPosition).SqrMagnitude() <= minDistance * minDistance;
    }

    public override void OnUpdate()
    {
        //AttackOnOverlapCollision();
        AttackOnPassingObjects();
        oldPos = currentPos;
    }

    private void AttackOnOverlapCollision()
    {
        int overlapFound = GetDamageableObjectByOverlapPoint(attackPoint.position, overlapCollisions);

        for (int i = 0; i < overlapFound; i++)
        {
            IDamageable obj = overlapCollisions[i].GetComponent<IDamageable>();
            if (obj != null)
                weapon.OnHitObject(attackPoint.position, obj);
        }
    }

    private int GetDamageableObjectByOverlapPoint(Vector2 position, Collider2D[] overlapCollisions)
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = mask;
        return collider.OverlapCollider(filter, overlapCollisions);
    }

    private void AttackOnPassingObjects()
    {
        int raycastFound = GetObjectsByPassingObjects(oldPos, currentPos);
        for (int i = 0; i < raycastFound; i++)
        {
            if (raycastHits[i].transform == this.transform)
                continue;
            var obj = raycastHits[i].transform.GetComponent<IDamageable>();
            weapon.OnHitObject(raycastHits[i].point, obj);
        }
    }

    private int GetObjectsByPassingObjects(Vector2 oldPos, Vector2 currentPos)
    {
        ContactFilter2D filter = new ContactFilter2D();
        filter.layerMask = mask;
        return Physics2D.Linecast(oldPos, currentPos, filter, raycastHits);
    }

}