using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}