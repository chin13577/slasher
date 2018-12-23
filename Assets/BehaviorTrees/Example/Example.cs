using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree.Example
{ 
    public class Example : MonoBehaviour
    {
        public BehaviorTreeGraph brain;
        Coroutine agentRoutine;

        void Start()
        {
            agentRoutine = StartCoroutine(RunBotAlgorithm());
        }

        public void StopAI()
        {
            StopCoroutine(agentRoutine);
        }

        IEnumerator RunBotAlgorithm()
        {
            var root = brain.GetNode(this.gameObject);
            while (true)
            {
                var resultState = root.Evaluate();
                if (resultState == NodeStates.Success)
                    break;
                else if (resultState == NodeStates.Running)
                    yield return null;
                else
                    yield return new WaitForSeconds(0.2f);
            }
        }
    }

}
