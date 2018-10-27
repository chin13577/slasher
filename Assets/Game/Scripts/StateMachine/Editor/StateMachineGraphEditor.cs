using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Shinnii.StateMachine
{
    public class StateMachineGraphEditor : Editor
    {
        [MenuItem("Assets/Create/StateMachineGraph", priority = 0)]
        public static void CreateGraph()
        {
            StateMachineGraph graph = ScriptableObject.CreateInstance<StateMachineGraph>();

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            path = (path == "") ? AssetDatabase.GenerateUniqueAssetPath("Assets/StateMachineGraph.asset") :
                AssetDatabase.GenerateUniqueAssetPath(AssetDatabase.GetAssetPath(Selection.activeObject) + "/StateMachineGraph.asset");

            AssetDatabase.CreateAsset(graph, path);

            StartNode startState = graph.AddNode<StartNode>();
            startState.name = "Start";
            startState.position = new Vector2(-250, 0);

            AnyNode anyState = graph.AddNode<AnyNode>();
            anyState.name = "Any";
            anyState.position = new Vector2(-250, -250);

            graph.startState = startState;
            graph.anyState = anyState;

            AssetDatabase.AddObjectToAsset(startState, graph);
            AssetDatabase.AddObjectToAsset(anyState, graph);

            AssetDatabase.SaveAssets();
            Selection.activeObject = graph;
        }
    }
}