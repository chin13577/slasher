﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

namespace Shinnii.StateMachine
{
    [CreateNodeMenu("")]
    public class StartNode : FuryNode
    {
        public override StateType StateType { get { return StateType.Start; } }
        [Output] public FuryNode exit;
    }
}