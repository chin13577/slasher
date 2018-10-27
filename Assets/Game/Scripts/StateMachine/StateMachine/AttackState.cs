using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shinnii.Controller;

namespace Shinnii.StateMachine
{
    public class AttackState : CharacterState, IReceiveMovement, IReceiveAttackEnter//, IReceiveDamageEvent, IReceiveAttackEvent, IReceiveMovementEvent, IReceiveSkillEvent
    {
        public ReceiveInputTime receiveInputTime;
        public List<TriggerEvent> triggerEvents;

        private Node nextBluePrint;
        private AttackNode currentAttackNode;
        private Coroutine animRoutine;
        private Animator weaponAnimator;
        private bool isContinueAttack;
        public AttackState(StateMachineGraph graph, StateMachine machine) : base(graph, machine)
        {
            weaponAnimator = character.weapon.animator;
        }

        public override void Enter()
        {
            nextBluePrint = null;
            currentAttackNode = machine.GetCurrentNode() as AttackNode;
            this.info = currentAttackNode.info;
            this.receiveInputTime = currentAttackNode.receiveInputTime;
            this.triggerEvents = new List<TriggerEvent>(currentAttackNode.triggerEvents);

            character.AddListener((IReceiveMovement)this);
            character.AddListener((IReceiveAttackEnter)this);

            animRoutine = character.StartCoroutine(Animate());
        }

        public override void Exit()
        {
            weaponAnimator.CrossFade("Idle", 0.1f, 0, 0);
            isContinueAttack = false;
            if (animRoutine != null)
                character.StopCoroutine(animRoutine);

            character.RemoveListener((IReceiveMovement)this);
            character.RemoveListener((IReceiveAttackEnter)this);
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

        IEnumerator Animate()
        {
            weaponAnimator.CrossFade(info.stateName, info.transitionDuration,
                                    info.layerIndex, info.animationOffset);
            yield return null;
            while (weaponAnimator.IsInTransition(0))
            {
                yield return null;
            }

            float exitTime = info.hasExitTime ? info.exitTime : 1;
            while (weaponAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime <= exitTime)
            {

                if (isContinueAttack) { exitTime = currentAttackNode.exitTimeOnNextAttack; }
                ExcecuteTriggerEvent();
                yield return null;
            }
            ExcecuteTriggerEvent(true);
            yield return null;
            if (nextBluePrint == null)
                nextBluePrint = machine.GetCurrentNode().GetNextStateFromPort("exit");
            if (nextBluePrint != null)
                Finish();
        }

        private void ExcecuteTriggerEvent(bool forceExcicute = false)
        {
            for (int i = 0; i < triggerEvents.Count; i++)
            {
                if (forceExcicute || animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= triggerEvents[i].triggerNormalizeTime)
                {
                    machine.OnAnimationEventTrigger(triggerEvents[i].stateEvent, triggerEvents[i].param);
                    triggerEvents.Remove(triggerEvents[i]);
                    i--;
                }
            }
        }

        void IReceiveMovement.OnReceiveMovement(Vector2 direction, float power)
        {
            animator.SetFloat("MoveSpeed", power);
            character.Rotate(direction);
            character.Move(power);
        }

        void IReceiveAttackEnter.OnReceiveAttackEnter()
        {
            float currentNormalizeTime = weaponAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime;
            if (currentNormalizeTime > receiveInputTime.startTime && currentNormalizeTime < receiveInputTime.exitTime)
            {
                if (nextBluePrint == null)
                    nextBluePrint = machine.GetCurrentNode().GetNextStateFromPort("onAttack");
                if (nextBluePrint != null)
                    isContinueAttack = true;
            }
        }

        // void IReceiveSkillEvent.OnInputSkill()
        // {
        //     machine.SetAppendingSkill(machine.anyNode.FindConnectedNode(StateType.Skill) as SkillNode);
        // }

        // void IReceiveDamageEvent.OnTakeDamage(DamageData damageData)
        // {
        //     if ((DateTime.Now - pawn.lastHit).TotalSeconds <= pawn.status.ImmuneTime) return;
        //     pawn.lastHit = DateTime.Now;
        //     pawn.DealDamage(damageData.damage);

        //     if (pawn.IsDead)
        //     {
        //         machine.JumpToAnyState(StateType.Dead);
        //     }
        //     else
        //     { 
        //         if (damageData.interruptedType != InterruptedType.None)
        //         {
        //             ImmuneType immuneTo = currentAttackNode.immuneTo;
        //             bool immuneSuccess = false;
        //             foreach (ImmuneType type in Enum.GetValues(typeof(ImmuneType)))
        //             {
        //                 if (immuneTo.HasFlag(type))
        //                 {
        //                     if (damageData.interruptedType.ToString() == type.ToString())
        //                         immuneSuccess = true;
        //                 }
        //             }
        //             if (!immuneSuccess)
        //                 machine.OnInturrupted(damageData);
        //         }
        //     } 
        // }
    }
}