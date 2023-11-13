using Logic.Levels;
using Services.PersistentProgress;

namespace Services.StateMachine.States
{
    public class InitialState : BaseStates
    {
        private readonly LevelItems _levelItems;
        private readonly TrainingPanel _trainingPanel;
        private readonly SearchItem _searchItem;
        private readonly CurrentLevel _currentLevel;
        private readonly IPersistentProgressService _progressService;

        public InitialState(GameStateMachine stateMachine, LevelItems levelItems, TrainingPanel trainingPanel, SearchItem searchItem,
            CurrentLevel currentLevel, IPersistentProgressService progressService) : base(stateMachine)
        {
            _levelItems = levelItems;
            _trainingPanel = trainingPanel;
            _searchItem = searchItem;
            _currentLevel = currentLevel;
            _progressService = progressService;
        }

        public override void Enter()
        {
            _levelItems.SetNumberOfTasks(_progressService.UserProgress.Attempts[_currentLevel.LevelNumber - 1]);
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