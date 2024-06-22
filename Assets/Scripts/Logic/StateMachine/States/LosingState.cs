using TokaBoka.Services;
using TokaBoka.UI;

namespace TokaBoka.StateMachine
{
    public class LosingState : BaseStates
    {
        private readonly LevelUI _levelUI;
        
        public LosingState(GameStateMachine stateMachine, LevelUI levelUI)
            : base(stateMachine) => _levelUI = levelUI;

        public override void Enter()
        {
            _levelUI.ChangeButtonVisibility(visibility: false);
            _levelUI.ShowLossPanel(visibility: true);
        }

        public override void Exit() =>
            _levelUI.ShowLossPanel(visibility: false);
    }
}