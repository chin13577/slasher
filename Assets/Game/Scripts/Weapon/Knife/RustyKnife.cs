using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RustyKnife : Weapon
{
    public override void OnHitObject(Vector2 hitPos, IDamageable target)
    {
        if (Vector3.Dot(owner.direction, target.direction) >= 0)
        {
            if (target.CanAttacked)
            {
                target.TakeDamage(10);
                print("Hit");
            }
        }
        else
        {
            var pushable = target.GetTransform().GetComponent<IPushable>();
            if (pushable != null)
            {
                Vector2 forceDirection = (target.position.ToVector2() - hitPos).normalized;
                print(forceDirection);
                pushable.Push(/*target.direction.ToVector2() + */forceDirection * 100);
                print("push");
            }
        }
    }
}
