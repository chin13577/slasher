using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Shinnii.StateMachine
{
    [CreateNodeMenu("Node/WeaponBranchNode")]
    public class WeaponBranchNode : Node
    {
        public override StateType StateType { get { return StateType.WeaponBranch; } }
        [Input] public Node enter;
        [Output] public Node Bow;
        [Output] public Node Dagger;
        [Output] public Node Fist;
        [Output] public Node Gun;
        [Output] public Node Staff;
        [Output] public Node Sword;
    }
}