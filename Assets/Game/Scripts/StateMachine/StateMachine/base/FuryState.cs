using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinnii.StateMachine
{
    public abstract class CharacterState
    {
        public AnimationInfo info;
        public StateMachineGraph graph;
        public StateMachine machine;

        protected Character character;
        protected Animator animator;
        public CharacterState(StateMachineGraph graph, StateMachine machine)
        {
            this.graph = graph;
            this.machine = machine;
            this.character = machine.character;
            this.animator = machine.animator;
        }

        public abstract void Enter();
        public abstract void Exit();

        public virtual void Finish()
        {
            machine.UpdateState();
        }
        public abstract CharacterState GetNext();
    }
}