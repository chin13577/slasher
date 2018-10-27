using System;
using System.Collections;
using System.Collections.Generic;
using Shinnii.Controller;
using UnityEngine;

namespace Shinnii.StateMachine
{
    public class StableState : CharacterState, IReceiveMovement, IReceiveAttackEnter, IReceiveDashEvent// , IReceiveDamageEvent ,IReceiveSkillEvent
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
            animator.SetFloat("MoveSpeed", 0);
            animator.CrossFade(info.stateName, info.transitionDuration, info.layerIndex, info.animationOffset);
        }

        public override void Exit()
        {
            character.RemoveListener((IReceiveMovement)this);
            character.RemoveListener((IReceiveAttackEnter)this);
            character.RemoveListener((IReceiveDashEvent)this);
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

        /*/ 

void IReceiveMovementEvent.OnDash()
{
  if (!pawn.IsDashable) return;
  nextNode = machine.GetCurrentNode().GetNextStateFromPort("onDash");
  if (nextNode != null)
  {
      Finish();
  }
}

void IReceiveMovementEvent.OnFlick(Vector3 direction, float power)
{
  if (!pawn.IsDashable) return;
  nextNode = machine.GetCurrentNode().GetNextStateFromPort("onDash");
  if (nextNode != null)
  {
      Finish();
  }
}

void IReceiveAttackEvent.OnAttack()
{
  nextNode = machine.GetCurrentNode().GetNextStateFromPort("onAttack");
  if (nextNode != null)
  {
      Finish();
  }
}

void IReceiveDamageEvent.OnTakeDamage(DamageData damageData)
{
  if ((DateTime.Now - pawn.lastHit).TotalSeconds <= pawn.status.ImmuneTime) return;
  pawn.lastHit = DateTime.Now;
  pawn.DealDamage(damageData.damage);
  if (pawn.IsDead)
  {
      machine.JumpToAnyState(StateType.Dead);
  }
  else
  {
      if (damageData.interruptedType != InterruptedType.None)
      {
          StableNode idleNode = machine.GetCurrentNode() as StableNode;
          ImmuneType immuneTo = idleNode.immuneTo;
          bool immuneSuccess = false;
          foreach (ImmuneType type in Enum.GetValues(typeof(ImmuneType)))
          {
              if (immuneTo.HasFlag(type))
              {
                  if (damageData.interruptedType.ToString() == type.ToString())
                      immuneSuccess = true;
              }
          }
          if (!immuneSuccess)
              machine.OnInturrupted(damageData);
      }
  }
}

void IReceiveSkillEvent.OnInputSkill()
{ 
  machine.JumpToAnyState(StateType.Skill);
}*/
    }
}