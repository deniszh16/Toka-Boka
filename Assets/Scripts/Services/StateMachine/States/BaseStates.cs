﻿namespace Services.StateMachine.States
{
    public abstract class BaseStates : IState
    {
        public readonly GameStateMachine _stateMachine;

        protected BaseStates(GameStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        public abstract void Enter();
        public abstract void Exit();
    }
}