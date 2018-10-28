using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shinnii.StateMachine
{
    public sealed class StateMachine
    {
        public Character character;
        public Animator animator;

        private StateMachineGraph graph;
        private Node currentNode;
        private StartNode startNode;
        public AnyNode anyNode;

        private CharacterState currentState;
        private Dictionary<StateType, CharacterState> stateDict = new Dictionary<StateType, CharacterState>();

        #region BlackDoor
        public DamageData currentDamageData;
        #endregion

        public CharacterState GetState(StateType type)
        {
            return stateDict[type];
        }

        public Node GetCurrentNode()
        {
            return currentNode;
        }

        public void SetCurrentNode(Node node)
        {
            currentNode = node;
        }

        public StateMachine(StateMachineGraph graph, Character character)
        {
            this.graph = graph;
            this.character = character;
            this.animator = character.animator;

            startNode = graph.GetNode(StateType.Start) as StartNode;
            anyNode = graph.GetNode(StateType.Any) as AnyNode;
            InitializeStateMachineDictionary();
            currentNode = startNode;
            currentState = stateDict[StateType.Start];
            currentState.Enter();
        }

        private void InitializeStateMachineDictionary()
        {
            for (int i = 0; i < graph.nodes.Count; i++)
            {
                var bluePrintState = graph.nodes[i] as Node;
                var stateType = bluePrintState.StateType;
                if (!stateDict.ContainsKey(stateType))
                {
                    CharacterState state = CreateFuryState(stateType);
                    if (state != null)
                        stateDict.Add(stateType, state);
                }
            }
        }

        public void Update()
        {
            if (currentState != null)
                currentState.Update();
        }

        public void UpdateState()
        {
            if (currentState != null)
                currentState.Exit();
            currentState = currentState.GetNext();

            if (currentState != null)
                currentState.Enter();
        }

        public CharacterState CreateFuryState(StateType type)
        {
            switch (type)
            {
                case StateType.Any:
                    return null;
                case StateType.Attack:
                    return new AttackState(graph, this);
                case StateType.Dash:
                    return new DashState(graph, this);
                case StateType.Start:
                    return new StartState(graph, this);
                case StateType.Stable:
                    return new StableState(graph, this);
                case StateType.Struggle:
                    return new StruggledState(graph, this);
                case StateType.Stun:
                    return new StunnedState(graph, this);
                case StateType.WeaponBranch:
                    return new WeaponBranchState(graph, this);
                default:
                    throw new NotImplementedException();
            }
        }

        public void OnAnimationEventTrigger(StateEvent stateEvent, int param)
        {
            switch (stateEvent)
            {
                case StateEvent.AttackStart:
                    break;
                case StateEvent.AttackTrigger:
                    character.OnAnimationAttackTrigger(param);
                    break;
                case StateEvent.AttackEnd:
                    break;
                default:
                    break;
            }

        }

        public void JumpToAnyState(StateType state)
        {
            Node node = anyNode.FindConnectedNodeByStateType(state);
            if (node != null)
            {
                SetCurrentNode(node);
                if (currentState != null)
                    currentState.Exit();
                currentState = GetState(state);
                currentState.Enter();
            }
        }

        public void OnInturrupted(DamageData damageData)
        {
            if (damageData.interruptedType == InterruptedType.None)
                return;

            this.currentDamageData = damageData;
            StateType targetStateType = 0;
            if (damageData.interruptedType == InterruptedType.Struggle)
                targetStateType = StateType.Struggle;
            else if (damageData.interruptedType == InterruptedType.Stun)
                targetStateType = StateType.Stun;

            JumpToAnyState(targetStateType);
        }

    }
}