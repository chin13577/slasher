using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

namespace  BehaviorTree
{
    public class BehaviorTreeGraph : XNode.NodeGraph
    {
        [HideInInspector]
        public BehaviorTreeBlueprint root; 

        public BehaviorTreeBlueprint GetNode(NodeType type)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                BehaviorTreeBlueprint node = nodes[i] as BehaviorTreeBlueprint;
                if (type == node.NodeType)
                    return node;
            }
            return null;
        } 
    }
}
