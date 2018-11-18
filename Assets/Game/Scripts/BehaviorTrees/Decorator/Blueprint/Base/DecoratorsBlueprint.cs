using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace BehaviorTree
{
    public enum DecoratorsNodeType { TimeLimit }
    public abstract class DecoratorsBlueprint : BehaviorTreeBlueprint
    {
        public override NodeType NodeType { get { return NodeType.Decorator; } }
        public abstract DecoratorsNodeType DecoratorsNodeType { get; }

    }
}