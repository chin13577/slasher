using BehaviorTree;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Example : MonoBehaviour
{
    public BehaviorTreeGraph brain;
    Coroutine agentRoutine;
    // Use this for initialization
    void Start()
    {
        agentRoutine = StartCoroutine(RunBotAlgorithm());
    }

    IEnumerator RunBotAlgorithm()
    {
        var root = brain.GetNode();
        while (true)
        {
            var resultState = root.Evaluate();
            Debug.Log(resultState);
            if (resultState == NodeStates.Running)
                yield return null;
            else
                yield return new WaitForSeconds(0.2f);
        }
    }
}
