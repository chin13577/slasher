using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : Character
{
    public override void OnAnimationAttackTrigger(int param)
    {
        weapon.OnAttack(param);
    }

    public override void Update()
    {
        base.Update();

        if (base.TargetOnSight)
        {
            Vector2 direction = (TargetOnSight.transform.position - transform.position).ToVector2().normalized;
            UpdateAttackDirection(direction);
        }
    }

    public override void Move(Vector2 direction, float power)
    {
        base.Move(direction, power);
        //base.UpdateAttackDirection(direction);
    }
}