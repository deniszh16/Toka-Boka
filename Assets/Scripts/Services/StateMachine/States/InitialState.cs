using Logic.Levels;

namespace Services.StateMachine.States
{
    public class InitialState : BaseStates
    {
        private readonly LevelItems _levelItems;

        public InitialState(GameStateMachine stateMachine, LevelItems levelItems) : base(stateMachine) =>
            _levelItems = levelItems;

        public override void Enter() =>
            _levelItems.SelectElementsForTask();

        public override void Exit()
        {
        }
    }
}