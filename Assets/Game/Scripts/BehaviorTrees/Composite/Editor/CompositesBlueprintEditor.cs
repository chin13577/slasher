using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace BehaviorTree
{
    [CustomNodeEditor(typeof(CompositesBlueprint))]
    public class CompositesBlueprintEditor : NodeEditor
    {
        public override void OnBodyGUI()
        {
            base.OnBodyGUI();
            CompositesBlueprint composite = target as CompositesBlueprint;

            if (GUILayout.Button("ReAlignSequensesOrder"))
            {
                composite.ReAlignSequensesOrder();
            }
        } 
    }
}