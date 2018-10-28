using System;
using System.Collections;
using System.Collections.Generic;
using Shinnii.Controller;
using UnityEngine;

namespace Shinnii.StateMachine
{
    public class StableState : CharacterState, IReceiveMovement, IReceiveAttackEnter, IReceiveDashEvent, IReceiveDamageEvent// ,IReceiveSkillEvent
    {
        private Node nextNode;

        public StableState(StateMachineGraph graph, StateMachine machine) : base(graph, machine)
        {
        }

        public override void Enter()
        {
            this.info = machine.GetCurrentNode().info;
            character.AddListener((IReceiveMovement)this);
            character.AddListener((IReceiveAttackEnter)this);
            character.AddListener((IReceiveDashEvent)this);
            character.AddListener((IReceiveDamageEvent)this);
            animator.SetFloat("MoveSpeed", 0);
            animator.CrossFade(info.stateName, info.transitionDuration, info.layerIndex, info.animationOffset);
        }

        public override void Exit()
        {
            animator.SetFloat("MoveSpeed", 0);
            character.RemoveListener((IReceiveMovement)this);
            character.RemoveListener((IReceiveAttackEnter)this);
            character.RemoveListener((IReceiveDashEvent)this);
            character.RemoveListener((IReceiveDamageEvent)this);
        }

        public override CharacterState GetNext()
        {
            if (nextNode != null)
            {
                StateType nextStateType = nextNode.StateType;
                machine.SetCurrentNode(nextNode);
                return machine.GetState(nextStateType);
            }
            return null;
        }

        void IReceiveMovement.OnReceiveMovement(Vector2 direction, float power)
        {
            animator.SetFloat("MoveSpeed", power);
            character.Rotate(direction);
            character.Move(power);
        }

        void IReceiveDashEvent.OnReceiveDashEvent()
        {
            if (character.IsDash) return;
            nextNode = machine.GetCurrentNode().GetNextStateFromPort("onDash");
            if (nextNode != null)
            {
                Finish();
            }
        }
        void IReceiveAttackEnter.OnReceiveAttackEnter()
        {
            nextNode = machine.GetCurrentNode().GetNextStateFromPort("onAttack");
            if (nextNode != null)
            {
                Finish();
            }
        }

        void IReceiveDamageEvent.OnTakeDamage(DamageData damageData)
        {
            if ((DateTime.Now - character.lastHit).TotalSeconds <= character.status.ImmuneTime) return;
            character.lastHit = DateTime.Now;
            character.DealDamage(damageData.damage);
            if (character.IsDead)
            {
                machine.JumpToAnyState(StateType.Dead);
            }
            else
            {
                if (damageData.interruptedType != InterruptedType.None)
                {
                    machine.OnInturrupted(damageData);
                }
            }
        }

        /*/ 
         
         
void IReceiveSkillEvent.OnInputSkill()
{ 
  machine.JumpToAnyState(StateType.Skill);
}*/
    }
}