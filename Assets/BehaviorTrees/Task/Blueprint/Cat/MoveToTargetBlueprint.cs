using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using XNode;
using System;
using BattleCore;
using Shinnii.Controller;

[CreateNodeMenu("BehaviorTree/TasksNode/MoveToTargetNode")]
public class MoveToTargetBlueprint : TasksBlueprint
{
    public override TasksNodeType TasksNodeType { get { return TasksNodeType.None; } }

    public float minDist;
    [Range(0, 1f)] public float power;
    [Input] public BehaviorTreeBlueprint input;

    public override BehaviorTreeNode GetNode(GameObject owner)
    {
        MoveToTargetNode node = new MoveToTargetNode(owner);
        node.minDist = minDist;
        node.power = power;
        return node;
    }
}

[Serializable]
public class MoveToTargetNode : BehaviorTreeNode
{
    GameObject target;
    Character ownerCharacter;
    SensorController sensor;

    public float minDist, power;
    public MoveToTargetNode(GameObject owner) : base(owner)
    {
        ownerCharacter = owner.GetComponent<Character>();
        if (ownerCharacter == null)
        {
            Debug.LogWarning("SeekNode: Not found Component 'Character' in gameObject.");
        }
        sensor = ownerCharacter.sensor;
        sensor.OnObserveChange += Callback_OnObserveChange;
    }
    private void Callback_OnObserveChange(GameObject obj)
    {
        target = ownerCharacter.TrackingTarget;
    }

    public override NodeStates Evaluate()
    {
        if (target == null)
        {
            ((IReceiveMovement)ownerCharacter).OnReceiveMovement(ownerCharacter.Direction, 0);
            return m_nodeState = NodeStates.Failure;
        }

        float sqrMagnitudeDistance = Vector2.SqrMagnitude(target.transform.position - owner.transform.position);
        if (sqrMagnitudeDistance < minDist * minDist)
            return m_nodeState = NodeStates.Success;
        else
        {
            Vector2 direction = target.transform.position - ownerCharacter.transform.position;
            ((IReceiveMovement)ownerCharacter).OnReceiveMovement(direction, power);
            return m_nodeState = NodeStates.Running;
        }

    }

    public override void OnReset()
    {
        target = ownerCharacter.TrackingTarget;
        m_nodeState = NodeStates.Failure;
    }

    public override void OnComplete()
    {
        target = ownerCharacter.TrackingTarget;
        m_nodeState = NodeStates.Failure;
    }
}