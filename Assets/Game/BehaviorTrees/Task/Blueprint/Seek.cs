using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using XNode;
using System;
using BattleCore;
using Shinnii.Controller;

[CreateNodeMenu("BehaviorTree/TasksNode/SeekNode")]
public class Seek : TasksBlueprint
{
    public override TasksNodeType TasksNodeType { get { return TasksNodeType.None; } }

    public NodeStates state;
    [Input] public BehaviorTreeBlueprint input;

    public override BehaviorTreeNode GetNode(GameObject owner)
    {
        SeekNode logStateNode = new SeekNode(owner);
        return logStateNode;
    }
}

[Serializable]
public class SeekNode : BehaviorTreeNode
{
    GameObject target;
    SensorController sensor;
    Character ownerCharacter;
    public SeekNode(GameObject owner) : base(owner)
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
        target = obj;
    }

    public override NodeStates Evaluate()
    {
        if (target == null)
            return NodeStates.Failure;

        Vector2 direction = target.transform.position - ownerCharacter.transform.position;
        ((IReceiveMovement)ownerCharacter).OnReceiveMovement(direction, 0);
        return NodeStates.Success;
    }

}