using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinnii.StateMachine
{
    public class AttackState : CharacterState//, IReceiveDamageEvent, IReceiveAttackEvent, IReceiveMovementEvent, IReceiveSkillEvent
    {
        public ReceiveInputTime receiveInputTime;
        public List<TriggerEvent> triggerEvents;

        private FuryNode nextBluePrint;
        private AttackNode currentAttackNode;
        private Coroutine animRoutine;
        private List<Coroutine> triggerEventRoutines = new List<Coroutine>();


        public AttackState(StateMachineGraph graph, StateMachine machine) : base(graph, machine)
        {
        }

        public override void Enter()
        {
            nextBluePrint = null;
            this.info = machine.GetCurrentNode().info;
            currentAttackNode = machine.GetCurrentNode() as AttackNode;
            this.receiveInputTime = currentAttackNode.receiveInputTime;
            this.triggerEvents = new List<TriggerEvent>(currentAttackNode.triggerEvents);

            // pawn.AddListener((IReceiveDamageEvent)this);
            // pawn.AddListener((IReceiveMovementEvent)this);
            // pawn.AddListener((IReceiveAttackEvent)this);
            // pawn.AddListener((IReceiveSkillEvent)this);
            animRoutine = character.StartCoroutine(Animate());
        }

        public override void Exit()
        {
            if (animRoutine != null)
                character.StopCoroutine(animRoutine);
            for (int i = 0; i < triggerEventRoutines.Count; i++)
            {
                if (triggerEventRoutines[i] != null)
                    character.StopCoroutine(triggerEventRoutines[i]);
            }
            triggerEventRoutines.Clear();
            // pawn.RemoveListener((IReceiveDamageEvent)this);
            // pawn.RemoveListener((IReceiveMovementEvent)this);
            // pawn.RemoveListener((IReceiveAttackEvent)this);
            // pawn.RemoveListener((IReceiveSkillEvent)this);
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
            animator.CrossFade(info.stateName, info.transitionDuration, info.layerIndex, info.animationOffset);
            yield return null;
            while (animator.IsInTransition(0))
            {
                yield return null;
            }
            for (int i = 0; i < triggerEvents.Count; i++)
            {
                triggerEventRoutines.Add(character.StartCoroutine(ExcecuteEventTrigger(triggerEvents[i])));
            }

            float exitTime = info.hasExitTime ? info.exitTime : 1;
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= exitTime)
                yield return null;
            if (nextBluePrint == null)
                nextBluePrint = machine.GetCurrentNode().GetNextStateFromPort("exit");
            if (nextBluePrint != null)
                Finish();
        }

        IEnumerator ExcecuteEventTrigger(TriggerEvent triggerEvent)
        {
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= triggerEvent.triggerNormalizeTime)
                yield return null;
            machine.OnAnimationEventTrigger(triggerEvent.stateEvent);
        }

        // void IReceiveAttackEvent.OnAttack()
        // {
        //     float currentNormalizeTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
        //     if (currentNormalizeTime > receiveInputTime.startTime && currentNormalizeTime < receiveInputTime.exitTime)
        //     {
        //         if (nextBluePrint == null)
        //             nextBluePrint = machine.GetCurrentNode().GetNextStateFromPort("onAttack");
        //     }
        // }

        // void IReceiveMovementEvent.OnControl(Vector3 direction, float power)
        // {
        //     animator.SetFloat("MoveSpeed", power);
        //     if (pawn.IsDash) return;
        //     if (!pawn.IsMovable) return;
        //     pawn.currentDirection = direction;
        //     pawn.currentPower = power;
        // }

        // void IReceiveMovementEvent.OnDash() { }
        // void IReceiveMovementEvent.OnFlick(Vector3 direction, float power) { }

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