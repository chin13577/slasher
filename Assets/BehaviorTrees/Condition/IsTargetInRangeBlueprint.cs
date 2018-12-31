using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using XNode;
using System;
using BattleCore;
using Shinnii.Controller;

[CreateNodeMenu("BehaviorTree/ConditionNode/IsTargetInRangeNode")]
public class IsTargetInRangeBlueprint : ConditionBlueprint
{
    public override ConditionNodeType ConditionNodeType { get { return ConditionNodeType.None; } }

    public float minDist;
    [Input] public BehaviorTreeBlueprint input;

    public override BehaviorTreeNode GetNode(GameObject owner)
    {
        IsTargetInRangeNode node = new IsTargetInRangeNode(owner);
        node.minDist = minDist;
        return node;
    }
}

[Serializable]
public class IsTargetInRangeNode : BehaviorTreeNode
{
    public float minDist;
    GameObject target;
    Character ownerCharacter;
    public IsTargetInRangeNode(GameObject owner) : base(owner)
    {
        ownerCharacter = owner.GetComponent<Character>();
        if (ownerCharacter == null)
        {
            Debug.LogWarning("IsTargetInRangeNode: Not found Component 'Character' in gameObject.");
        }
    }

    public override NodeStates Evaluate()
    {
        target = ownerCharacter.TrackingTarget;
        if (target == null)
            return m_nodeState = NodeStates.Failure;

        float sqrMagnitudeDistance = Vector2.SqrMagnitude(target.transform.position - owner.transform.position); 
        if (sqrMagnitudeDistance < minDist * minDist)
            return m_nodeState = NodeStates.Success;
        else
            return m_nodeState = NodeStates.Failure;
    }

    public override void OnReset()
    {
        target = null;
        m_nodeState = NodeStates.Failure;
    }

    public override void OnComplete()
    {
        target = null;
        m_nodeState = NodeStates.Failure;
    }
}