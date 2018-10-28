using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinnii.StateMachine
{
    [CreateNodeMenu("Node/StruggledNode")]
    public class StruggledNode : Node
    {
        public override StateType StateType { get { return StateType.Struggle; } }

        [EnumFlagsAttribute]
        public ImmuneType immuneTo;
        [Input] public Node enter;
        [Output] public Node exit;
    }
}