using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BehaviorTree
{
    [Serializable]
    public class RootNode : BehaviorTreeNode
    {
        public BehaviorTreeNode next;

        public override NodeStates Evaluate()
        {
            return next.Evaluate();
        }
    }
}