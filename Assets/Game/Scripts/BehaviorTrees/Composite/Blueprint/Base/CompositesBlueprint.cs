using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace BehaviorTree
{
    public enum CompositesNodeType { Sequences, Selectors, Parallel }
    public abstract class CompositesBlueprint : BehaviorTreeBlueprint
    {
        public override NodeType NodeType { get { return NodeType.Composite; } }
        public abstract CompositesNodeType CompositesNodeType { get; }

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
    }
}