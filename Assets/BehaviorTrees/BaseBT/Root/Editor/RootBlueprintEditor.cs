using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace BehaviorTree
{
    [CustomNodeEditor(typeof(RootBlueprint))]
    public class RootBlueprintEditor : NodeEditor
    {
        public override void OnBodyGUI()
        {
            base.OnBodyGUI();
            RootBlueprint root = target as RootBlueprint;

            if (GUILayout.Button("ReAlignSequensesOrder"))
            {
                root.ReAlignSequensesOrder();
            }
        }
    }
}