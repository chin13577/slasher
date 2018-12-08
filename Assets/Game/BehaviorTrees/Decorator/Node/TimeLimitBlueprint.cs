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

        public override BehaviorTreeNode GetNode(GameObject owner)
        {
            TimeLimitNode timeLimitNode = new TimeLimitNode(owner);
            NodePort exitPort = GetOutputPort("exit");
            if (exitPort.Connection != null)
            {
                BehaviorTreeBlueprint blueprint = exitPort.Connection.node as BehaviorTreeBlueprint;
                timeLimitNode.next = blueprint.GetNode(owner);
            }
            return timeLimitNode;
        }
    }

    [Serializable]
    public class TimeLimitNode : BehaviorTreeNode
    {
        public BehaviorTreeNode next;

        public TimeLimitNode(GameObject owner) : base(owner)
        {
        }

        public override NodeStates Evaluate()
        {
            throw new NotImplementedException();
        }
    }

}
