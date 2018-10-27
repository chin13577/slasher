using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinnii.StateMachine
{

    public class WeaponBranchState : CharacterState
    {
        private Node nextBluePrint;
        public WeaponBranchState(StateMachineGraph graph, StateMachine machine) : base(graph, machine)
        {
        }

        public override void Enter()
        {
            nextBluePrint = null;
            string nextNode = character.weapon.WeaponType.ToString();
            if (nextBluePrint == null)
                nextBluePrint = machine.GetCurrentNode().GetNextStateFromPort(nextNode);
            if (nextBluePrint != null)
                Finish();
        }

        public override void Exit()
        {
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
    }
}