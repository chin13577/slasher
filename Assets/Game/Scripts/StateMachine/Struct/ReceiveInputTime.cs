using System;
using UnityEngine;

namespace Shinnii.StateMachine
{
    [Serializable]
    public struct ReceiveInputTime
    {
        /// <summary>
        /// start receive input in normalize time.
        /// </summary>
        [Range(0f, 1f)] public float startTime;

        /// <summary>
        /// stop receive input in normalize time.
        /// </summary>
        [Range(0f, 1f)] public float exitTime;
    }
}