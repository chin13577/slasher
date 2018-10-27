using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Shinnii.StateMachine
{
    [CreateNodeMenu("Node/StableNode")]
    public class StableNode : Node
    {
        public override StateType StateType { get { return StateType.Stable; } }

        [Input] public Node enter;
        [Output] public Node onAttack;
        [Output] public Node onDash;
    }
}