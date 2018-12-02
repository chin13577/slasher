using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace BehaviorTree
{
    [CreateNodeMenu("BehaviorTree/TasksNode/LogStateNode")]
    public class LogStateNodeBlueprint : TasksBlueprint
    {
        public override TasksNodeType TasksNodeType { get { return TasksNodeType.None; } }

        public NodeStates state;
        [Input] public BehaviorTreeBlueprint input;

        public override BehaviorTreeNode GetNode()
        {
            LogStateNode logStateNode = new LogStateNode();
            logStateNode.state = this.state;
            return logStateNode;
        }
    }
 
    [Serializable]
    public class LogStateNode : BehaviorTreeNode
    {
        public NodeStates state;

        public override NodeStates Evaluate()
        {
            return state;
        }
    }


}