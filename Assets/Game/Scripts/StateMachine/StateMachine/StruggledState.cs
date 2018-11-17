using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shinnii.Controller;

namespace Shinnii.StateMachine
{
    public class StruggledState : CharacterState, IReceiveDamageEvent, IReceiveMovement
    {
        private Node nextBluePrint;
        private Coroutine animRoutine;
        public StruggledState(StateMachineGraph graph, StateMachine machine) : base(graph, machine)
        {
        }

        public override void Enter()
        {
            nextBluePrint = null;
            character.IsStruggle = true; 

            this.info = machine.GetCurrentNode().info;
            character.AddListener((IReceiveMovement)this);
            character.AddListener((IReceiveDamageEvent)this);
            animRoutine = character.StartCoroutine(Animate());
            character.UpdateAttackDirection(machine.currentDamageData.damageDirection * -1f); 
        }

        public override void Exit()
        {
            character.IsStruggle = false;
            if (animRoutine != null)
                character.StopCoroutine(animRoutine);
            character.RemoveListener((IReceiveMovement)this);
            character.RemoveListener((IReceiveDamageEvent)this);
        }

        public override CharacterState GetNext()
        {
            if (nextBluePrint != null)
            {
                StateType nextStateType = nextBluePrint.StateType;
                machine.SetCurrentNode(nextBluePrint);
                return machine.GetState(nextStateType);
            }
            return null;
        }

        private IEnumerator Animate()
        {
            animator.CrossFade(info.stateName, info.transitionDuration, info.layerIndex, info.animationOffset);
            yield return null;
            while (animator.IsInTransition(0))
                yield return null;
            float exitTime = info.hasExitTime ? info.exitTime : 1;
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= exitTime)
                yield return null;
            if (nextBluePrint == null)
                nextBluePrint = machine.GetCurrentNode().GetNextStateFromPort("exit");
            if (nextBluePrint != null)
                Finish();
        }
         
        void IReceiveMovement.OnReceiveMovement(Vector2 direction, float power)
        {
            power = power / 2;
            animator.SetFloat("MoveSpeed", power);
            character.Direction = direction;
            character.UpdateAttackDirection(direction);
            character.Move(power);
        }

        void IReceiveDamageEvent.OnTakeDamage(DamageData damageData)
        { 
            if ((DateTime.Now - character.lastHit).TotalSeconds <= character.status.ImmuneTime) return;
            character.lastHit = DateTime.Now;
            character.DealDamage(damageData); 
            if (character.IsDead)
            {
                machine.JumpToAnyState(StateType.Dead);
            }
            else
            {
                if (damageData.interruptedType != InterruptedType.None)
                { 
                    StruggledNode struggledNode = machine.GetCurrentNode() as StruggledNode;
                    ImmuneType immuneTo = struggledNode.immuneTo;
                    bool immuneSuccess = false;
                    foreach (ImmuneType type in System.Enum.GetValues(typeof(ImmuneType)))
                    {
                        if (immuneTo.HasFlag(type))
                        { 
                            if (damageData.interruptedType.ToString() == type.ToString())
                                immuneSuccess = true;
                        }
                    } 
                    if (!immuneSuccess)
                    { 
                        machine.OnInturrupted(damageData);
                    } 
                }
            }
        }

    }
}
