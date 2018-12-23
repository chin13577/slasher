using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace BehaviorTree
{
    [CreateNodeMenu("BehaviorTree/DecoratorsNode/ToggleNode")]
    public class ToggleBlueprint : TasksBlueprint
    {

        public override TasksNodeType TasksNodeType { get { return TasksNodeType.None; } }

        [Input] public BehaviorTreeBlueprint input;
        [Output] public BehaviorTreeBlueprint exit;


        public override BehaviorTreeNode GetNode(GameObject owner)
        {
            ToggleNode node = new ToggleNode(owner);
            NodePort exitPort = GetOutputPort("exit");
            if (exitPort.Connection != null)
            {
                BehaviorTreeBlueprint blueprint = exitPort.Connection.node as BehaviorTreeBlueprint;
                node.next = blueprint.GetNode(owner);
            }
            return node;
        }
    }

    public class ToggleNode : BehaviorTreeNode
    {
        public BehaviorTreeNode next;
        private bool toggle;

        public ToggleNode(GameObject owner) : base(owner)
        {
        }

        public override NodeStates Evaluate()
        {
            if (next != null)
            {
                toggle = !toggle; 
                if (toggle)
                    return next.Evaluate();
                else
                    return NodeStates.Failure;
            }
            else
            {
                toggle = !toggle;
                if (toggle)
                    return NodeStates.Success;
                else
                    return NodeStates.Failure;
            }

        }

        public override void OnComplete()
        {
            if (next != null)
                next.OnComplete();
        }

        public override void OnReset()
        {
            toggle = false;
            if (next != null)
                next.OnReset();
        }
    }

}