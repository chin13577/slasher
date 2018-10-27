using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Shinnii.Controller;
using System;

namespace Shinnii.StateMachine
{

    public class DashState : CharacterState, IReceiveMovement
    {
        private Node nextBluePrint;
        private Coroutine animRoutine;
        private Coroutine countDownRountine;

        private Vector2 dashDirection;
        public DashState(StateMachineGraph graph, StateMachine machine) : base(graph, machine)
        {
        }
        public override void Enter()
        {
            nextBluePrint = null;
            this.info = machine.GetCurrentNode().info;
            character.IsDash = true;
            dashDirection = character.Direction; 
            character.AddListener((IReceiveMovement)this);

            animRoutine = character.StartCoroutine(Animate());
            countDownRountine = character.StartCoroutine(CountDown());
        }

        public override void Exit()
        {
            character.IsDash = false;
            character.RemoveListener((IReceiveMovement)this);
            character.rigid.velocity = Vector2.zero;
            if (animRoutine != null)
                character.StopCoroutine(animRoutine);
            if (countDownRountine != null)
                character.StopCoroutine(countDownRountine);
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
            animator.CrossFade(info.stateName, info.transitionDuration,
                                    info.layerIndex, info.animationOffset);
            yield return null;
            while (animator.IsInTransition(0))
            {
                yield return null;
            }

            float exitTime = info.hasExitTime ? info.exitTime : 1;
            while (animator.GetCurrentAnimatorStateInfo(0).normalizedTime <= exitTime)
            {
                yield return null;
            }
            if (nextBluePrint == null)
                nextBluePrint = machine.GetCurrentNode().GetNextStateFromPort("exit");
            if (nextBluePrint != null)
                Finish();
        }

        private IEnumerator CountDown()
        {
            float time = 0.25f;
            while (time > 0)
            {
                Vector3 newPos = character.transform.position + new Vector3(dashDirection.x, dashDirection.y, 0) * character.dashSpeed * 1 * Time.deltaTime;
                character.rigid.MovePosition(newPos);
                time -= Time.deltaTime;
                yield return null; 
            }
            character.rigid.velocity = Vector2.zero;
        }

        void IReceiveMovement.OnReceiveMovement(Vector2 direction, float power)
        {
            character.Rotate(direction);
        }
    }
}
