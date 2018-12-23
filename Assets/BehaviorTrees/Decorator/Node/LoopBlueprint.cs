using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace BehaviorTree
{
    [CreateNodeMenu("BehaviorTree/DecoratorsNode/LoopNode")]
    public class LoopBlueprint : TasksBlueprint
    {
        public override TasksNodeType TasksNodeType { get { return TasksNodeType.None; } }
        public int count;
        public bool repeatForever;
        [Input] public BehaviorTreeBlueprint input;
        [Output] public BehaviorTreeBlueprint exit;

        public override BehaviorTreeNode GetNode(GameObject owner)
        {
            LoopNode loopNode = new LoopNode(owner);
            loopNode.repeatForever = this.repeatForever;
            loopNode.successCount = this.count;

            NodePort exitPort = GetOutputPort("exit");
            if (exitPort.Connection != null)
            {
                BehaviorTreeBlueprint blueprint = exitPort.Connection.node as BehaviorTreeBlueprint;
                loopNode.next = blueprint.GetNode(owner);
            }
            return loopNode;
        }
    }

    public class LoopNode : BehaviorTreeNode
    {
        public BehaviorTreeNode next;
        public int successCount;
        public bool repeatForever;
        private int currentCount;
        public LoopNode(GameObject owner) : base(owner)
        {
        }

        public override NodeStates Evaluate()
        {
            if (next == null)
                return NodeStates.Failure;

            NodeStates resultState = next.Evaluate();

            if (repeatForever)
            {
                if (resultState == NodeStates.Success)
                {
                    if (next != null)
                        next.OnComplete();
                }
                else if (resultState == NodeStates.Failure)
                {
                    if (next != null)
                        next.OnReset();
                }
                return NodeStates.Running;
            }
            else
            {

                if (resultState == NodeStates.Success)
                {
                    if (currentCount >= successCount - 1)
                    {
                        currentCount = 0;
                        return NodeStates.Success;
                    }
                    if (next != null)
                        next.OnComplete();
                    currentCount++;
                }
                else if (resultState == NodeStates.Failure)
                {
                    if (next != null)
                        next.OnReset();
                }

                return NodeStates.Running;
            }


        }

        public override void OnComplete()
        {
            currentCount = 0;
            if (next != null)
                next.OnComplete();
        }

        public override void OnReset()
        {
            currentCount = 0;
            if (next != null)
                next.OnReset();
        }
    }

}