using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XNode;

namespace Shinnii.StateMachine
{
    public enum StateType { Attack, Any, Branch, Start, Stable, Dash, Dead, Skill, Struggle, Stun, WeaponBranch }
    public abstract class Node : XNode.Node
    {
        public AnimationInfo info;
        public abstract StateType StateType { get; }
        private StateMachineGraph _graph;
        protected StateMachineGraph Graph
        {
            get
            {
                if (_graph == null)
                    _graph = graph as StateMachineGraph;
                return _graph;
            }
        }

        public Node GetNextStateFromPort(string portName)
        {
            NodePort exitPort = GetOutputPort(portName);
            if (exitPort.IsConnected)
                return exitPort.Connection.node as Node;
            else
                return null;
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }

    }
}