using Logic.Levels;

namespace Services.StateMachine.States
{
    public class InitialState : BaseStates
    {
        private readonly LevelItems _levelItems;
        private readonly TrainingPanel _trainingPanel;
        private readonly SearchItem _searchItem;

        public InitialState(GameStateMachine stateMachine, LevelItems levelItems,
            TrainingPanel trainingPanel, SearchItem searchItem) : base(stateMachine)
        {
            _levelItems = levelItems;
            _trainingPanel = trainingPanel;
            _searchItem = searchItem;
        }

        public override void Enter()
        {
            _levelItems.SelectElementsForTask();
            _searchItem.ShowCurrentItem();
            CheckTraining();
        }

        private void CheckTraining()
        {
            if (_trainingPanel.Training) _stateMachine.Enter<TrainingState>();
            else _stateMachine.Enter<PlayState>();
        }

        public override void Exit()
        {
        }
    }
}