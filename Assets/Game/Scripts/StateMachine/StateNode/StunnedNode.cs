using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XNode;

namespace Shinnii.StateMachine
{ 
    [CreateNodeMenu("Node/StunnedNode")]
    public class StunnedNode : Node
    {
        public override StateType StateType { get { return StateType.Stun; } }

        [EnumFlagsAttribute]
        public ImmuneType immuneTo;
        [Input] public Node enter;
        [Output] public Node exit;
    }
}