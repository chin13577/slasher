using System;
using UnityEngine;

namespace Shinnii.StateMachine
{
    [Serializable]
    public struct AnimationInfo
    {
        public string stateName;
        [Range(0f, 1f)] public float transitionDuration;
        [Range(0f, 1f)] public float animationOffset;
        public int layerIndex;
        public bool hasExitTime;
        /// <summary>
        /// exit time in normalize
        /// </summary>
        [Range(0f, 1f)] public float exitTime;
    }
}