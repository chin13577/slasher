using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    [CreateNodeMenu("BehaviorTree/TasksNode/Log")]
    public class LogBlueprint : TasksBlueprint
    {
        public override TasksNodeType TasksNodeType { get { return TasksNodeType.None; } }

        public string log;
        [Input] public BehaviorTreeBlueprint input;

        public override BehaviorTreeNode GetNode(GameObject owner)
        {
            LogNode logNode = new LogNode(owner);
            logNode.log = log;
            return logNode;
        }
    }

    
    [Serializable]
    public class LogNode : BehaviorTreeNode
    {
        public string log = "";

        public LogNode(GameObject owner) : base(owner)
        {
        }

        public override NodeStates Evaluate()
        {
            Debug.Log(log);
            return NodeStates.Success;
        }
    } 
} 