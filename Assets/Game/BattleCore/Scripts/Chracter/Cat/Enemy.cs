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
        BehaviorTreeNode root = brain.GetNode(this.gameObject);
        // BehaviorTreeNode root = this.createGuardEnemy01Decision();
        while (!IsDead)
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

    private BehaviorTreeNode createGuardEnemy01Decision()
    {
        BehaviorTreeNode root = new RootNode(this.gameObject);
        SequencesNode sequenceNode = new SequencesNode(this.gameObject);
        sequenceNode.nexts.Add(new IsHasTargetNode(this.gameObject));
        sequenceNode.nexts.Add(new MoveToTargetNode(this.gameObject) { minDist = 0.8f, power = 1 });
        ((RootNode)root).next = sequenceNode;
        return root;
    }

    public override void OnAnimationAttackTrigger(int param)
    {
        weapon.OnAttack(param);
    }
}
