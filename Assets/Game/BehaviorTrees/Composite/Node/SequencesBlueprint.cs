using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XNode;

namespace BehaviorTree
{
    [CreateNodeMenu("BehaviorTree/CompositesNode/SequencesNode")]
    public class SequencesBlueprint : CompositesBlueprint
    {
        public override CompositesNodeType CompositesNodeType { get { return CompositesNodeType.Sequences; } }

        [Input] public BehaviorTreeBlueprint input;
        [Output] public BehaviorTreeBlueprint exit;

        public override BehaviorTreeNode GetNode()
        {
            SequencesNode sequencesNode = new SequencesNode();
            NodePort exitPort = GetOutputPort("exit");
            List<BehaviorTreeNode> nodes = new List<BehaviorTreeNode>();
            for (int i = 0; i < exitPort.ConnectionCount; i++)
            {
                var blueprint = exitPort.GetConnection(i).node as BehaviorTreeBlueprint;
                nodes.Add(blueprint.GetNode());
            }
            sequencesNode.nexts = nodes;
            return sequencesNode;
        }
    }


    [Serializable]
    public class SequencesNode : BehaviorTreeNode
    {
        public List<BehaviorTreeNode> nexts = new List<BehaviorTreeNode>();

        public override NodeStates Evaluate()
        {
            foreach (BehaviorTreeNode node in nexts)
            {
                switch (node.Evaluate())
                {
                    case NodeStates.Failure:
                        m_nodeState = NodeStates.Failure;
                        return m_nodeState;
                    case NodeStates.Success:
                        m_nodeState = NodeStates.Success;
                         continue;
                    case NodeStates.Running:
                        m_nodeState = NodeStates.Running;
                        return NodeStates.Running;
                    default:
                        m_nodeState = NodeStates.Success;
                        return m_nodeState;
                }
            }
            return m_nodeState;
        }
    }

}
