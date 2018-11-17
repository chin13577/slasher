using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Shinnii.Senses
{
    public abstract class Sense : MonoBehaviour, IDisposable
    {
        protected SensorController controller;

        public abstract void Initialize(SensorController controller);

        public abstract void OnUpdate();
        public abstract void Dispose();
    }
}