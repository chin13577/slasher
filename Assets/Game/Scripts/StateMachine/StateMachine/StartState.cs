using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinnii.StateMachine
{
    public class StartState : CharacterState
    {
        public StartState(StateMachineGraph graph, StateMachine machine) : base(graph, machine)
        {
        }

        public override void Enter()
        {
            Finish();
        }

        public override void Exit()
        {
        }

        public override CharacterState GetNext()
        {

            FuryNode nextBluePrint = machine.GetCurrentNode().GetNextStateFromPort("exit");
            if (nextBluePrint != null)
            {
                machine.SetCurrentNode(nextBluePrint);
                StateType nextStateType = nextBluePrint.StateType;
                return machine.GetState(nextStateType);
            }
            return null;
        }
    }
}