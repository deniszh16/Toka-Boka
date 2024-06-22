using TokaBoka.Services;
using TokaBoka.UI;

namespace TokaBoka.StateMachine
{
    public class TrainingState : BaseStates
    {
        private readonly TrainingPanel _trainingPanel;
        
        public TrainingState(GameStateMachine stateMachine, TrainingPanel trainingPanel)
            : base(stateMachine) => _trainingPanel = trainingPanel;

        public override void Enter()
        {
            _trainingPanel.TrainingCompleted += CompleteTraining;
            _trainingPanel.ChangePanelVisibility(activity: true);
        }

        private void CompleteTraining() =>
            _stateMachine.Enter<PlayState>();

        public override void Exit()
        {
            _trainingPanel.TrainingCompleted -= CompleteTraining;
            _trainingPanel.ChangePanelVisibility(activity: false);
        }
    }
}