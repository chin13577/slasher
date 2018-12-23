using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace BehaviorTree
{
    [CreateNodeMenu("BehaviorTree/DecoratorsNode/InverterNode")]
    public class InverterBlueprint : TasksBlueprint
    {

        public override TasksNodeType TasksNodeType { get { return TasksNodeType.None; } }

        [Input] public BehaviorTreeBlueprint input;
        [Output] public BehaviorTreeBlueprint exit;


        public override BehaviorTreeNode GetNode(GameObject owner)
        {
            InverterNode inverterNode = new InverterNode(owner);
            NodePort exitPort = GetOutputPort("exit");
            if (exitPort.Connection != null)
            {
                BehaviorTreeBlueprint blueprint = exitPort.Connection.node as BehaviorTreeBlueprint;
                inverterNode.next = blueprint.GetNode(owner);
            }
            return inverterNode;
        }
    }

    public class InverterNode : BehaviorTreeNode
    {
        public BehaviorTreeNode next;

        public InverterNode(GameObject owner) : base(owner)
        {
        }

        public override NodeStates Evaluate()
        {
            if (next == null)
                return NodeStates.Failure;

            NodeStates resultState = next.Evaluate();
            if (resultState == NodeStates.Failure)
                return NodeStates.Success;
            else if (resultState == NodeStates.Success)
                return NodeStates.Failure;
            else
                return resultState;
        }

        public override void OnComplete()
        {
            if (next != null)
                next.OnComplete();
        }

        public override void OnReset()
        {
            if (next != null)
                next.OnReset();
        }
    }

}