using Services.StateMachine.States;
using Services.StateMachine;
using Logic.UI.Levels;

namespace Logic.StateMachine.States
{
    public class PauseState : BaseStates
    {
        private readonly LevelUI _levelUI;
        
        public PauseState(GameStateMachine stateMachine, LevelUI levelUI)
            : base(stateMachine) => _levelUI = levelUI;

        public override void Enter()
        {
            _levelUI.ChangeButtonVisibility(visibility: false);
            _levelUI.ChangeVisibilityOfScreenDimming(state: true);
            _levelUI.ChangePausePanelVisibility(visibility: true);
        }

        public override void Exit()
        {
            _levelUI.ChangeButtonVisibility(visibility: true);
            _levelUI.ChangeVisibilityOfScreenDimming(state: false);
            _levelUI.ChangePausePanelVisibility(visibility: false);
        }
    }
}