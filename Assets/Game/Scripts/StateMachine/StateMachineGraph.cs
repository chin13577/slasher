using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace Shinnii.StateMachine
{
    public class StateMachineGraph : XNode.NodeGraph
    {
        [HideInInspector]
        public Node startState;
        [HideInInspector]
        public Node anyState;

        public Node GetNode(StateType type)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                Node node = nodes[i] as Node;
                if (type == node.StateType)
                    return node;
            }
            return null;
        }
    }
}
