using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XNode;

namespace Shinnii.StateMachine
{
    [CreateNodeMenu("FuryState/AttackState")]
    public class AttackNode : FuryNode
    {
        public override StateType StateType { get { return StateType.Attack; } }
        public ReceiveInputTime receiveInputTime;
        public List<TriggerEvent> triggerEvents = new List<TriggerEvent>();

        public ImmuneType immuneTo;
        [Input] public FuryNode enter;
        [Output] public FuryNode exit;
        [Output] public FuryNode onAttack;
    }
}