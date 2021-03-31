using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using XNode;
using System;
using BattleCore;
using Shinnii.Controller;

[CreateNodeMenu("BehaviorTree/TasksNode/LookAtTargetNode")]
public class LookAtTargetBlueprint : TasksBlueprint
{
    public override TasksNodeType TasksNodeType { get { return TasksNodeType.None; } }

    [Input] public BehaviorTreeBlueprint input;

    public override BehaviorTreeNode GetNode(GameObject owner)
    {
        LookAtTargetNode node = new LookAtTargetNode(owner);
        return node;
    }
}

[Serializable]
public class LookAtTargetNode : BehaviorTreeNode
{
    GameObject target;
    Character ownerCharacter;
    public LookAtTargetNode(GameObject owner) : base(owner)
    {
        ownerCharacter = owner.GetComponent<Character>();
        if (ownerCharacter == null)
        {
            Debug.LogWarning("SeekNode: Not found Component 'Character' in gameObject.");
        }
    }

    public override NodeStates Evaluate()
    {
        target = ownerCharacter.TargetOnSight;
        if (target == null)
            return m_nodeState = NodeStates.Failure;
        Vector2 direction = target.transform.position - ownerCharacter.transform.position;
        ((IReceiveMovement)ownerCharacter).OnReceiveMovement(direction, 0);
        ownerCharacter.UpdateAttackDirection(direction);
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