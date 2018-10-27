using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XNode;


namespace Shinnii.StateMachine
{
    [CreateNodeMenu("Node/DashNode")]
    public class DashNode : Node
    {
        public override StateType StateType { get { return StateType.Dash; } }

        [Input] public Node enter;
        [Output] public Node exit;
    }
}