using Logic.Levels;

namespace Services.StateMachine.States
{
    public class LosingState : BaseStates
    {
        private readonly LevelResults _levelResults;
        
        public LosingState(GameStateMachine stateMachine, LevelResults levelResults)
            : base(stateMachine) =>
            _levelResults = levelResults;

        public override void Enter() =>
            _levelResults.ShowLossPanel(visibility: true);

        public override void Exit() =>
            _levelResults.ShowLossPanel(visibility: false);
    }
}