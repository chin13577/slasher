using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BattleCore;

public class Fist : Weapon
{
    public override WeaponType WeaponType { get { return WeaponType.Fist; } }

    public override void OnAttack(int param)
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(owner.AttackPosition, 0.3f, owner.Direction, 0, owner.maskEnemy);
        if (hits != null && hits.Length > 0)
        {
            for (int i = 0; i < hits.Length; i++)
            {
                IDamageable damageable = hits[i].transform.GetComponent<IDamageable>();
                if (damageable != null && hits[i].rigidbody != owner.rigid)
                {
                    if (damageable.GetTeam() == owner.team) continue;
                    float damage = owner.status.attack + Random.Range(-1, 3);
                    if (param == 1)
                        damage = damage * 0.75f; 
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
