using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace BehaviorTree
{
    public enum TasksNodeType { None }
    public abstract class TasksBlueprint : BehaviorTreeBlueprint
    {
        public override NodeType NodeType { get { return NodeType.Task; } }
        public abstract TasksNodeType TasksNodeType { get; }

    }
}