using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace BehaviorTree
{
    public class BehaviorTreesGraphEditor : Editor
    {
        [MenuItem("Assets/Create/BehaviorTreesGraphEditor", priority = 0)]
        public static void CreateGraph()
        {
            BehaviorTreeGraph graph = ScriptableObject.CreateInstance<BehaviorTreeGraph>();

            string path = AssetDatabase.GetAssetPath(Selection.activeObject);
            path = (path == "") ? AssetDatabase.GenerateUniqueAssetPath("Assets/BehaviorTreeGraph.asset") :
                AssetDatabase.GenerateUniqueAssetPath(AssetDatabase.GetAssetPath(Selection.activeObject) + "/BehaviorTreeGraph.asset");

            AssetDatabase.CreateAsset(graph, path);

            RootBlueprint rootNode = graph.AddNode<RootBlueprint>();
            rootNode.name = "Root";
            rootNode.position = new Vector2(-250, 0);
             
            graph.root = rootNode; 

            AssetDatabase.AddObjectToAsset(rootNode, graph); 

            AssetDatabase.SaveAssets();
            Selection.activeObject = graph;
        }
    }
}