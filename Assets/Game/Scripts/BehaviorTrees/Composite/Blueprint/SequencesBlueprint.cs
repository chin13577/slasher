using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
}