using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Shinnii.Controller;

namespace Shinnii.StateMachine
{

    public class DashState : CharacterState, IReceiveMovement
    {
        private Node nextBluePrint;
        private Coroutine animRoutine;
        private float dashTime;
        public DashState(StateMachineGraph graph, StateMachine machine) : base(graph, machine)
        {
        }
        public override void Enter()
        {
            nextBluePrint = null;
            this.info = machine.GetCurrentNode().info;
            character.IsDash = true;
            dashTime = 0.2f;
            character.rigid.velocity = character.Direction * character.dashSpeed;
            character.AddListener((IReceiveMovement)this);
            animRoutine = character.StartCoroutine(Animate());
        }

        public override void Update()
        {
            dashTime -= Time.deltaTime;
            if (dashTime < 0)
            {
                character.rigid.velocity = Vector2.zero;
            }
        }

        public override void Exit()
        {
            character.IsDash = false;
            character.RemoveListener((IReceiveMovement)this);
            character.rigid.velocity = Vector2.zero;
            if (animRoutine != null)
                character.StopCoroutine(animRoutine);
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

        void IReceiveMovement.OnReceiveMovement(Vector2 direction, float power)
        {
            character.Rotate(direction);
        }
    }
}
