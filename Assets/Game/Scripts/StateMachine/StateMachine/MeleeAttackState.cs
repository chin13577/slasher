using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Shinnii.Controller;

namespace Shinnii.StateMachine
{
    public class MeleeAttackState : CharacterState, IReceiveMovement, IReceiveAttackEnter//, IReceiveDamageEvent, IReceiveAttackEvent, IReceiveMovementEvent, IReceiveSkillEvent
    {
        public ReceiveInputTime receiveInputTime;
        public List<TriggerEvent> triggerEvents;

        private Node nextBluePrint;
        private MeleeAttackNode currentAttackNode;
        private Coroutine animRoutine;
        private Coroutine dashRoutine;
        private Animator weaponAnimator;
        private bool isContinueAttack;
        public MeleeAttackState(StateMachineGraph graph, StateMachine machine) : base(graph, machine)
        {
            weaponAnimator = character.weapon.animator;
        }

        public override void Enter()
        {
            nextBluePrint = null;
            currentAttackNode = machine.GetCurrentNode() as MeleeAttackNode;
            this.info = currentAttackNode.info;
            this.receiveInputTime = currentAttackNode.receiveInputTime;
            this.triggerEvents = new List<TriggerEvent>(currentAttackNode.triggerEvents);

            character.AddListener((IReceiveMovement)this);
            character.AddListener((IReceiveAttackEnter)this);

            animRoutine = character.StartCoroutine(Animate());
            dashRoutine = character.StartCoroutine(AttackDash());


            if (character.IsTracking)
            {
                character.UpdateAttackDirection(character.AttackDirection);
            }
        }

        public override void Exit()
        {
            weaponAnimator.CrossFade("Idle", 0.1f, 0, 0);
            character.IsAttacking = false;
            isContinueAttack = false;
            if (animRoutine != null)
                character.StopCoroutine(animRoutine);
            if (dashRoutine != null)
                character.StopCoroutine(dashRoutine);

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

        IEnumerator AttackDash()
        {
            character.IsAttacking = true;
            float duration = 0.35f;
            float moveTime = 0;
            float normalizedTime = 0;
            Vector2 targetPos = character.transform.position.ToVector2() + character.AttackDirection * 0.8f;
            do
            {
                normalizedTime = moveTime / duration;
                Vector2 newPos = Vector2.Lerp(character.transform.position, targetPos, normalizedTime);
                character.rigid.MovePosition(newPos);
                yield return null;
                moveTime += Time.deltaTime;
            } while (normalizedTime <= 1);
            character.IsAttacking = false;
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
                if (forceExcicute || weaponAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= triggerEvents[i].triggerNormalizeTime)
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
            character.Direction = direction;
            character.Move(power / 2.5f);
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