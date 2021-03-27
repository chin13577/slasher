using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class Enemy : Character
{
    private const float BRAIN_REFRESH_INTERVAL = 0.2f;
    public BehaviorTreeGraph brain;

    void Start()
    {
        StartCoroutine(RunBehaviorTree());
    }

    private IEnumerator RunBehaviorTree()
    {
        var root = brain.GetNode(this.gameObject);
        while (true && !IsDead)
        {
            var resultState = root.Evaluate();
            if (resultState == NodeStates.Running)
                yield return null; // wait for next frame.
            else if (resultState == NodeStates.Success)
                yield return null;
            else
                yield return new WaitForSeconds(BRAIN_REFRESH_INTERVAL);
        }
        yield break;
    }

    public override void OnAnimationAttackTrigger(int param)
    {
        weapon.OnAttack(param);
    }
}
