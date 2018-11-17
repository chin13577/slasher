using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    public override void OnAnimationAttackTrigger(int param)
    {
        weapon.OnAttack(param);
    }
}