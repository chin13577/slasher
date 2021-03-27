using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using XNode;
using System;
using BattleCore;
using Shinnii.Controller;

[CreateNodeMenu("BehaviorTree/ConditionNode/IsHasTargetNode")]
public class IsHasTargetBlueprint : ConditionBlueprint
{
    public override ConditionNodeType ConditionNodeType { get { return ConditionNodeType.None; } }

    [Input] public BehaviorTreeBlueprint input;

    public override BehaviorTreeNode GetNode(GameObject owner)
    {
        IsHasTargetNode node = new IsHasTargetNode(owner);
        return node;
    }
}

[Serializable]
public class IsHasTargetNode : BehaviorTreeNode
{
    GameObject target;
    Character ownerCharacter;
    public IsHasTargetNode(GameObject owner) : base(owner)
    {
        ownerCharacter = owner.GetComponent<Character>();
        if (ownerCharacter == null)
        {
            Debug.LogWarning("IsTargetInRangeNode: Not found Component 'Character' in gameObject.");
        }
    }

    public override NodeStates Evaluate()
    {
        target = ownerCharacter.TargetOnSight;
        if (target == null)
            return m_nodeState = NodeStates.Failure;
        else
            return m_nodeState = NodeStates.Success;
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