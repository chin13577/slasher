using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using XNode;

namespace Shinnii.StateMachine
{
    [CreateNodeMenu("Node/Attack/MeleeAttackNode")]
    public class MeleeAttackNode : Node
    {
        public override StateType StateType { get { return StateType.MeleeAttack; } }
        public float exitTimeOnNextAttack;
        public ReceiveInputTime receiveInputTime;
        public List<TriggerEvent> triggerEvents = new List<TriggerEvent>();


        [Input] public Node enter;
        [Output] public Node exit;
        [Output] public Node onAttack;
    }
}