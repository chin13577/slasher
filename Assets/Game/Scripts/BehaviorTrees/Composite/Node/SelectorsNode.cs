using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BehaviorTree
{
    [Serializable]
    public class SelectorsNode : BehaviorTreeNode
    {
        public List<BehaviorTreeNode> nexts = new List<BehaviorTreeNode>();

        public override NodeStates Evaluate()
        {
            foreach (BehaviorTreeNode node in nexts)
            {
                switch (node.Evaluate())
                {
                    case NodeStates.Failure: continue;
                    case NodeStates.Success:
                        m_nodeState = NodeStates.Success;
                        return m_nodeState;
                    case NodeStates.Running:
                        m_nodeState = NodeStates.Running;
                        return m_nodeState;
                    default: continue;
                }
            }
            m_nodeState = NodeStates.Failure;
            return m_nodeState;
        }
    }
}