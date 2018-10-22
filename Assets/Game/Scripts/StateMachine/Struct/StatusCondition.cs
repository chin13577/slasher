using System;
using UnityEngine;

namespace Shinnii.StateMachine
{
    [Serializable]
    public struct StatusCondition
    {
        [SerializeField]
        public enum StatusType { HP, ATK }
        public StatusType type;
        public enum Operator { Greater, Less, Equals, NotEqual}
        public Operator operand;
        public int value;
        public bool checkByPercent;
    }
}