using System;
using UnityEngine;

namespace Shinnii.StateMachine
{
    public enum StateEvent { None, AttackStart, AttackEnd, AttackTrigger, SkillCastingStart, SkillTrigger, SkillCastingEnd }
    [Serializable]
    public struct TriggerEvent
    {
        public StateEvent stateEvent;
        [Range(0f, 1f)] public float triggerNormalizeTime;
        public int param;
    }
}
