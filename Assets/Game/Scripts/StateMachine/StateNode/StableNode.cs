using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Shinnii.StateMachine
{
    [CreateNodeMenu("FuryState/StableNode")]
    public class StableNode : FuryNode
    {
        public ImmuneType immuneTo;
        public override StateType StateType { get { return StateType.Stable; } }

        [Input] public FuryNode enter;
        [Output] public FuryNode onAttack;
        [Output] public FuryNode onDash;
    }
}