using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public class BehaviorTreeGraph : XNode.NodeGraph
    {
        [HideInInspector]
        public BehaviorTreeBlueprint root;

        public BehaviorTreeNode GetNode()
        {
            return root.GetNode();
        }
    }
}
