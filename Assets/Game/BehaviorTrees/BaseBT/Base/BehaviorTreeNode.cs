using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BehaviorTree
{
    public enum NodeStates { Success, Failure, Running }

    [Serializable]
    public abstract class BehaviorTreeNode
    {
        /* Delegate that returns the state of the node.*/
        public delegate NodeStates NodeReturn();
        /* The current state of the node */
        protected NodeStates m_nodeState;
        public NodeStates nodeState { get { return m_nodeState; } }
        public GameObject owner;
        /* The constructor for the node */
        public BehaviorTreeNode(GameObject owner)
        {
            this.owner = owner;
        }
        /* Implementing classes use this method to evaluate the desired set of conditions */
        public abstract NodeStates Evaluate();

    }
}