using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorTree;
using UnityEngine;

public class Enemy : Character
{
    public BehaviorTreeGraph brain;

    void Start()
    {
        StartCoroutine(StartAIAlgorithm());
    }

    private IEnumerator StartAIAlgorithm()
    {
        var root = brain.GetNode(this.gameObject);
        while (!IsDead)
        {
            var resultState = root.Evaluate();
            if (resultState == NodeStates.Running || resultState == NodeStates.Success)
                yield return null;
            else
                yield return new WaitForSeconds(0.2f);
        }
    }

    public override void OnAnimationAttackTrigger(int param)
    {
        weapon.OnAttack(param);
    }
}
