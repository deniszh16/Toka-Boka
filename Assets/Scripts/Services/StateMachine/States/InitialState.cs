using Logic.Levels;

namespace Services.StateMachine.States
{
    public class InitialState : BaseStates
    {
        private readonly LevelItems _levelItems;
        private readonly TrainingPanel _trainingPanel;

        public InitialState(GameStateMachine stateMachine, LevelItems levelItems, TrainingPanel trainingPanel)
            : base(stateMachine)
        {
            _levelItems = levelItems;
            _trainingPanel = trainingPanel;
        }

        public override void Enter()
        {
            _levelItems.SelectElementsForTask();
            CheckTraining();
        }

        private void CheckTraining()
        {
            if (_trainingPanel.Training)
                _stateMachine.Enter<TrainingState>();
            else
                _stateMachine.Enter<PlayState>();
        }

        public override void Exit()
        {
        }
    }
}