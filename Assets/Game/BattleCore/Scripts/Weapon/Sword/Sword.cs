﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Weapon
{
    public override WeaponType WeaponType { get { return WeaponType.Sword; } }
    public override void OnAttack(int param)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(owner.AttackPosition, 1, owner.Direction, 0, owner.maskEnemy);
        List<IDamageable> defenders = new List<IDamageable>(); 
         
        if (hits != null && hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                IDamageable damageable = hits[i].transform.GetComponent<IDamageable>();
                if (defenders.Contains(damageable)) 
                    continue; 
                else
                    defenders.Add(damageable);

                if (damageable != null && hits[i].rigidbody != owner.rigid)
                {
                    if (damageable.GetTeam() == owner.team) continue;
                    float damage = owner.status.attack + Random.Range(-1, 3); 
                    damageable.TakeDamage(new DamageData()
                    {
                        damage = damage,
                        damageDirection = owner.Direction,
                        interruptedType = InterruptedType.Struggle
                    });
                }
            }
        }
    }
}
