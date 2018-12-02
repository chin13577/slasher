using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XNode;

namespace BehaviorTree
{
    [CreateNodeMenu("BehaviorTree/CompositesNode/SelectorsNode")]
    public class SelectorsBlueprint : CompositesBlueprint
    {
        public override CompositesNodeType CompositesNodeType { get { return CompositesNodeType.Selectors; } }

        [Input] public BehaviorTreeBlueprint input;
        [Output] public BehaviorTreeBlueprint exit;

        public override BehaviorTreeNode GetNode()
        {
            SelectorsNode selectorsNode = new SelectorsNode();
            NodePort exitPort = GetOutputPort("exit");
            List<BehaviorTreeNode> nodes = new List<BehaviorTreeNode>();
            for (int i = 0; i < exitPort.ConnectionCount; i++)
            {
                var blueprint = exitPort.GetConnection(i).node as BehaviorTreeBlueprint;
                nodes.Add(blueprint.GetNode());
            }
            selectorsNode.nexts = nodes;
            return selectorsNode;
        }
    } 


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