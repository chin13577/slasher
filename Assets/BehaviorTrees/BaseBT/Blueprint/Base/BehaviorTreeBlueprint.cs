using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace BehaviorTree
{
    public enum NodeType { Composite, Decorator, Condition, Task, Root }
    public abstract class BehaviorTreeBlueprint : XNode.Node
    {
        public abstract NodeType NodeType { get; }
        private BehaviorTreeGraph _graph;
        protected BehaviorTreeGraph Graph
        {
            get
            {
                if (_graph == null)
                    _graph = graph as BehaviorTreeGraph;
                return _graph;
            }
        }
        public abstract BehaviorTreeNode GetNode(GameObject owner);

        public BehaviorTreeBlueprint GetNextStateFromPort(string portName)
        {
            NodePort exitPort = GetOutputPort(portName);
            if (exitPort.IsConnected)
                return exitPort.Connection.node as BehaviorTreeBlueprint;
            else
                return null;
        }

        public NodeType GetTypeFromPort(string portName)
        {
            NodePort exitPort = GetOutputPort(portName);
            if (exitPort.IsConnected)
                return ((BehaviorTreeBlueprint)exitPort.Connection.node).NodeType;
            else
                return 0;
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }

    }
}