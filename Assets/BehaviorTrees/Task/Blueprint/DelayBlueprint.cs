using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    [CreateNodeMenu("BehaviorTree/TasksNode/Delay")]
    public class DelayBlueprint : TasksBlueprint
    {
        public override TasksNodeType TasksNodeType { get { return TasksNodeType.None; } }

        public float delay;
        [Input] public BehaviorTreeBlueprint input;

        public override BehaviorTreeNode GetNode(GameObject owner)
        {
            DelayNode delayNode = new DelayNode(owner);
            delayNode.delay = delay;
            return delayNode;
        }
    }


    [Serializable]
    public class DelayNode : BehaviorTreeNode
    {
        public float delay;
        private float timeStamp;
        public DelayNode(GameObject owner) : base(owner)
        {
            m_nodeState = NodeStates.Failure;
        }

        public override NodeStates Evaluate()
        {
            if (m_nodeState == NodeStates.Failure)
            {
                UpdateTimeStamp();
                m_nodeState = NodeStates.Running;
            }
            if (Time.time - timeStamp > delay)
            {
                UpdateTimeStamp();
                m_nodeState = NodeStates.Success;
            }
            return m_nodeState;
        }

        private void UpdateTimeStamp()
        {
            timeStamp = Time.time;
        }

        public override void OnInturupted()
        {
            ResetValue();
        }

        public override void OnReset()
        {
            ResetValue();
        }

        public override void OnComplete()
        {
            ResetValue();
        }

        private void ResetValue()
        {
            m_nodeState = NodeStates.Failure;
            timeStamp = 0;
        }
    }
}