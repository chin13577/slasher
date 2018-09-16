using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RustyKnife : Weapon
{
    public override void OnHitObject(IDamageable target)
    { 
        if (Vector3.Dot(owner.direction, target.direction) >= 0)
        {
            if (target.CanAttack)
                print("Hit");
        }
        else
            print("push");
    }
}
