using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace Shinnii.StateMachine
{
    public class StateMachineGraph : XNode.NodeGraph
    {
        [HideInInspector]
        public FuryNode startState;
        [HideInInspector]
        public FuryNode anyState;

        public FuryNode GetNode(StateType type)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                FuryNode node = nodes[i] as FuryNode;
                if (type == node.StateType)
                    return node;
            }
            return null;
        }
    }
}
