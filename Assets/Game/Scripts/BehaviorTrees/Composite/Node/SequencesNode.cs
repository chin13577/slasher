using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BehaviorTree
{
    [Serializable]
    public class SequencesNode : BehaviorTreeNode
    {
        public List<BehaviorTreeNode> nexts = new List<BehaviorTreeNode>();

        public override NodeStates Evaluate()
        {
            bool anyChildRunning = false;
            foreach (BehaviorTreeNode node in nexts)
            {
                switch (node.Evaluate())
                {
                    case NodeStates.Failure:
                        m_nodeState = NodeStates.Failure;
                        return m_nodeState;
                    case NodeStates.Success: continue;
                    case NodeStates.Running:
                        anyChildRunning = true;
                        continue;
                    default:
                        m_nodeState = NodeStates.Success;
                        return m_nodeState;
                }
            }
            m_nodeState = anyChildRunning ? NodeStates.Running : NodeStates.Success;
            return m_nodeState;
        }
    }
}
