﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dagger : Weapon
{
    public override WeaponType WeaponType { get { return WeaponType.Dagger; } }
    public override void OnAttack(int param)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(owner.AttackPosition, 1, owner.Direction, 0, owner.maskEnemy);
        if (hits != null && hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                IDamageable damageable = hits[i].transform.GetComponent<IDamageable>();
                if (damageable != null && hits[i].rigidbody != owner.rigid)
                {
                    if (damageable.GetTeam() == owner.team) continue;
                    float damage = owner.status.attack;
                    damageable.TakeDamage(new DamageData() { damage = damage });
                }
            }
        }
    }
}
