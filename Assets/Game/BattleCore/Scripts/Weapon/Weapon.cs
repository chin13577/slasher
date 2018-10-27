using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Bow, Dagger, Fist, Gun, Staff, Sword }
public abstract class Weapon : MonoBehaviour
{
    public Animator animator;
    public abstract WeaponType WeaponType { get; }
}
