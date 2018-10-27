using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Bow, Dagger, Fist, Gun, Staff, Sword }
public abstract class Weapon : MonoBehaviour
{
    protected Character owner;
    public Animator animator;
    public void Initialize(Character owner)
    {
        this.owner = owner;
    }

    public abstract WeaponType WeaponType { get; }

    public abstract void OnAttack(int param);
}
