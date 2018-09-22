using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public Character owner;
    public WeaponData data;

    public virtual void OnHitObject(Vector2 hitPos, IDamageable target) { }

}
