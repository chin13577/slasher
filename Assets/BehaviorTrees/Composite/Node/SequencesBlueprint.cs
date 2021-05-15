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

        public override BehaviorTreeNode GetNode(GameObject owner)
        {
            SequencesNode sequencesNode = new SequencesNode(owner);
            NodePort exitPort = GetOutputPort("exit");
            List<BehaviorTreeNode> nodes = new List<BehaviorTreeNode>();
            for (int i = 0; i < exitPort.ConnectionCount; i++)
            {
                var blueprint = exitPort.GetConnection(i).node as BehaviorTreeBlueprint;
                nodes.Add(blueprint.GetNode(owner));
            }
            sequencesNode.nexts = nodes;
            return sequencesNode;
        }
    }


    [Serializable]
    public class SequencesNode : BehaviorTreeNode
    {
        public List<BehaviorTreeNode> nexts = new List<BehaviorTreeNode>();
        private int currentIndex = 0;

        public SequencesNode(GameObject owner) : base(owner)
        {
        }

        public override NodeStates Evaluate()
        {
            for (int i = currentIndex; i < nexts.Count; i++)
            {
                BehaviorTreeNode currentNode = nexts[i];
                switch (currentNode.Evaluate())
                {
                    case NodeStates.Failure:
                        m_nodeState = NodeStates.Failure;
                        CheckInturrupted(i);
                        return m_nodeState;
                    case NodeStates.Success:
                        m_nodeState = NodeStates.Success;
                        continue;
                    case NodeStates.Running:
                        m_nodeState = NodeStates.Running;
                        // if currentNode is running so that,
                        // we continue run only this node until return success or fail. 
                        currentIndex = i;
                        return NodeStates.Running;
                    default:
                        m_nodeState = NodeStates.Success;
                        currentIndex++;
                        return m_nodeState;
                }
            }
            return m_nodeState;
        }

        private void CheckInturrupted(int currentIndex)
        {
            this.currentIndex = 0;
            for (int i = currentIndex + 1; i < nexts.Count; i++)
            {
                if (nexts[i].nodeState != NodeStates.Failure)
                    nexts[i].OnInturupted();
            }
        }

        public override void OnReset()
        {
            this.currentIndex = 0;
            for (int i = 0; i < nexts.Count; i++)
            {
                nexts[i].OnReset();
            }
        }

        public override void OnComplete()
        {
            this.currentIndex = 0;
            for (int i = 0; i < nexts.Count; i++)
            {
                nexts[i].OnComplete();
            }
        }
    }

}
