using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BehaviorTree
{
    [Serializable]
    public class TimeLimitNode : BehaviorTreeNode
    {
        public BehaviorTreeNode next;

        public override NodeStates Evaluate()
        {
            throw new NotImplementedException();
        }
    }
}
