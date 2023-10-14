using Logic.Levels;

namespace Services.StateMachine.States
{
    public class PauseState : BaseStates
    {
        private readonly GamePause _gamePause;
        
        public PauseState(GameStateMachine stateMachine, GamePause gamePause)
            : base(stateMachine) => _gamePause = gamePause;

        public override void Enter()
        {
            _gamePause.ChangeButtonVisibility(visibility: false);
            _gamePause.ChangePausePanelVisibility(visibility: true);
        }

        public override void Exit()
        {
            _gamePause.ChangeButtonVisibility(visibility: true);
            _gamePause.ChangePausePanelVisibility(visibility: false);
        }
    }
}