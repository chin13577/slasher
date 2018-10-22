using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Shinnii.StateMachine
{
    [CreateNodeMenu("")]
    public class AnyNode : FuryNode
    {
        public override StateType StateType { get { return StateType.Any; } }
        [Output] public FuryNode exit;

        public FuryNode FindConnectedNode(StateType target)
        {
            NodePort exitPort = GetOutputPort("exit");

            for (int i = 0; i < exitPort.ConnectionCount; i++)
            {
                var node = exitPort.GetConnection(i).node as FuryNode;
                if (node.StateType == target)
                    return node;
            }
            return null;
        }
    }
}