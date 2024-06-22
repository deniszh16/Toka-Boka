namespace DZGames.TokaBoka.Services
{
    public abstract class BaseStates : IState
    {
        protected readonly GameStateMachine _stateMachine;

        protected BaseStates(GameStateMachine stateMachine) =>
            _stateMachine = stateMachine;

        public abstract void Enter();
        public abstract void Exit();
    }
}