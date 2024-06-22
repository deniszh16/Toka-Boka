using System;
using System.Collections.Generic;

namespace DZGames.TokaBoka.Services
{
    public class GameStateMachine
    {
        private IState ActiveState { get; set; }
        private Dictionary<Type, IState> _states;

        public GameStateMachine() =>
            _states = new Dictionary<Type, IState>();

        public void AddState<TState>(TState state) where TState : class, IState =>
            _states.Add(typeof(TState), state);

        public void Enter<TState>() where TState : class, IState
        {
            IState state = ChangeState<TState>();
            state?.Enter();
        }
        
        private TState ChangeState<TState>() where TState : class, IState
        {
            ActiveState?.Exit();

            TState state = GetState<TState>();
            ActiveState = state;
            return state;
        }
        
        private TState GetState<TState>() where TState : class, IState =>
            _states[typeof(TState)] as TState;
    }
}