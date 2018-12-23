using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using XNode;
using System;
using BattleCore;
using Shinnii.Controller;

[CreateNodeMenu("BehaviorTree/TasksNode/AttackNode")]
public class AttackBlueprint : TasksBlueprint
{
    public override TasksNodeType TasksNodeType { get { return TasksNodeType.None; } }

    [Input] public BehaviorTreeBlueprint input;

    public override BehaviorTreeNode GetNode(GameObject owner)
    {
        AttackNode node = new AttackNode(owner);
        return node;
    }
}

[Serializable]
public class AttackNode : BehaviorTreeNode
{
    private Character ownerCharacter;
    private bool hasAttacked;
    public AttackNode(GameObject owner) : base(owner)
    {
        ownerCharacter = owner.GetComponent<Character>();
        if (ownerCharacter == null)
        {
            Debug.LogWarning("AttackNode: Not found Component 'Character' in gameObject.");
        }
    }

    public override NodeStates Evaluate()
    {
        if (!hasAttacked)
        {
            hasAttacked = true;
            ((IReceiveAttackEnter)ownerCharacter).OnReceiveAttackEnter();
            return m_nodeState = NodeStates.Success;
        }
        else
        {
            return m_nodeState = NodeStates.Failure;
        }
    }

    public override void OnReset()
    {
        hasAttacked = false;
        m_nodeState = NodeStates.Failure;
    }

    public override void OnComplete()
    { 
        hasAttacked = false;
        m_nodeState = NodeStates.Failure;
    }
}