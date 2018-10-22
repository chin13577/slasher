using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XNode;

namespace Shinnii.StateMachine
{
    public enum StateType { Start, Stable, Attack, Dash, Dead, Any, Branch, Skill, Struggle, Stun }
    public abstract class FuryNode : XNode.Node
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

        public FuryNode GetNextStateFromPort(string portName)
        {
            NodePort exitPort = GetOutputPort(portName);
            if (exitPort.IsConnected)
                return exitPort.Connection.node as FuryNode;
            else
                return null;
        }

        public override object GetValue(NodePort port)
        {
            return null;
        }

    }
}