using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RustyKnife : Weapon
{
    public override void OnHitObject(IDamageable target)
    {
        print("hit target");
    }
}
