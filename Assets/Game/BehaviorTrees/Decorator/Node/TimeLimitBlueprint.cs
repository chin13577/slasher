using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XNode;

namespace BehaviorTree
{
    [CreateNodeMenu("BehaviorTree/DecoratorsNode/TimeLimitNode")]
    public class TimeLimitBlueprint : DecoratorsBlueprint
    {
        public override DecoratorsNodeType DecoratorsNodeType { get { return DecoratorsNodeType.TimeLimit; } }
        public float time;

        [Input] public BehaviorTreeBlueprint input;
        [Output] public BehaviorTreeBlueprint exit;

        public override BehaviorTreeNode GetNode()
        {
            TimeLimitNode timeLimitNode = new TimeLimitNode();
            NodePort exitPort = GetOutputPort("exit");
            if (exitPort.Connection != null)
            {
                BehaviorTreeBlueprint blueprint = exitPort.Connection.node as BehaviorTreeBlueprint;
                timeLimitNode.next = blueprint.GetNode();
            }
            return timeLimitNode;
        }
    } 
    
    [Serializable]
    public class TimeLimitNode : BehaviorTreeNode
    {
        public BehaviorTreeNode next;

        public override NodeStates Evaluate()
        {
            throw new NotImplementedException();
        }
    }
 
}
