using System.Collections;
using System.Collections.Generic;
using XNode;

namespace BehaviorTree
{
    public enum ConditionNodeType { None }
    public abstract class ConditionBlueprint : BehaviorTreeBlueprint
    {
        public override NodeType NodeType { get { return NodeType.Condition; } }
        public abstract ConditionNodeType ConditionNodeType { get; }

    }
}