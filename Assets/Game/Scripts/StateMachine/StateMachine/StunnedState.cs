using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinnii.StateMachine
{
    public class StunnedState : CharacterState, IReceiveDamageEvent//  , IReceiveSkillEvent
    {
        private Node nextBluePrint;
        private Coroutine animRoutine;
        public StunnedState(StateMachineGraph graph, StateMachine machine) : base(graph, machine)
        {
        }

        public override void Enter()
        {
            nextBluePrint = null;
            character.IsStunned = true;
            character.currentPower = 0;

            this.info = machine.GetCurrentNode().info;
            character.AddListener((IReceiveDamageEvent)this);
            animRoutine = character.StartCoroutine(WaitForDuration(machine.currentDamageData.interruptedDuration));

        }

        public override void Exit()
        {
            character.IsStunned = false;
            if (animRoutine != null)
                character.StopCoroutine(animRoutine);
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

        private IEnumerator WaitForDuration(float duration)
        {
            animator.CrossFade(info.stateName, info.transitionDuration, info.layerIndex, info.animationOffset);
            yield return null;

            yield return new WaitForSeconds(duration);

            if (nextBluePrint == null)
                nextBluePrint = machine.GetCurrentNode().GetNextStateFromPort("exit");
            if (nextBluePrint != null)
                Finish();
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
                    StunnedNode stunNode = machine.GetCurrentNode() as StunnedNode;
                    ImmuneType immuneTo = stunNode.immuneTo;
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
                        machine.OnInturrupted(damageData);
                }
            }
        }


    }
}