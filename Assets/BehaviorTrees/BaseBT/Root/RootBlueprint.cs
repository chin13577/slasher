using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XNode;

namespace BehaviorTree
{
    [CreateNodeMenu("")]
    public class RootBlueprint : BehaviorTreeBlueprint
    {
        public override NodeType NodeType { get { return NodeType.Root; } }
        [Output] public BehaviorTreeBlueprint exit;

        public void ReAlignSequensesOrder()
        {
            NodePort exitPort = GetOutputPort("exit");
            List<NodePort> portList = new List<NodePort>();
            for (int i = 0; i < exitPort.ConnectionCount; i++)
            {
                portList.Add(exitPort.GetConnection(i));
            }
            portList.Sort((n1, n2) => { return n1.node.position.x.CompareTo(n2.node.position.x); });
            exitPort.ClearConnections();
            for (int i = 0; i < portList.Count; i++)
            {
                exitPort.Connect(portList[i]);
            }
        }

        public override BehaviorTreeNode GetNode(GameObject owner)
        {
            RootNode rootNode = new RootNode(owner);
            NodePort exitPort = GetOutputPort("exit");
            if (exitPort.Connection != null)
            {
                BehaviorTreeBlueprint blueprint = exitPort.Connection.node as BehaviorTreeBlueprint;
                rootNode.next = blueprint.GetNode(owner);
            }
            return rootNode;
        }
    }

    [Serializable]
    public class RootNode : BehaviorTreeNode
    {
        public BehaviorTreeNode next;

        public RootNode(GameObject owner) : base(owner)
        {
        }

        public override NodeStates Evaluate()
        {
            NodeStates result = next.Evaluate();
            if (result == NodeStates.Success)
                next.OnComplete();
            else if (result == NodeStates.Failure)
                next.OnReset();
            return result;
        }

        public override void OnComplete()
        {
        }

        public override void OnReset()
        {
        }
    }
}