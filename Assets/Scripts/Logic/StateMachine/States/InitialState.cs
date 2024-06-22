using DZGames.TokaBoka.Levels;
using DZGames.TokaBoka.Services;
using DZGames.TokaBoka.UI;

namespace DZGames.TokaBoka.StateMachine
{
    public class InitialState : BaseStates
    {
        private readonly IPersistentProgressService _progressService;

        private readonly CurrentLevel _currentLevel;
        private readonly LevelItems _levelItems;
        private readonly SearchItem _searchItem;
        private readonly LevelTimer _levelTimer;

        private readonly TrainingPanel _trainingPanel;
        private readonly ItemCounter _itemCounter;
        private readonly HintButton _hintButton;

        public InitialState(GameStateMachine stateMachine, IPersistentProgressService progressService, CurrentLevel currentLevel, LevelItems levelItems,
            SearchItem searchItem, LevelTimer levelTimer, TrainingPanel trainingPanel, ItemCounter itemCounter, HintButton hintButton) : base(stateMachine)
        {
            _progressService = progressService;
            
            _currentLevel = currentLevel;
            _levelItems = levelItems;
            _searchItem = searchItem;
            _levelTimer = levelTimer;
            
            _trainingPanel = trainingPanel;
            _itemCounter = itemCounter;
            _hintButton = hintButton;
        }

        public override void Enter()
        {
            int attempts = _progressService.GetUserProgress.Attempts[_currentLevel.LevelNumber - 1];
            
            _levelItems.SetNumberOfTasks(attempts);
            _levelItems.SelectElementsForTask();
            _levelTimer.SetDifficultyLevel(attempts);
            _levelTimer.SetTimer();
            
            _searchItem.Init(_currentLevel, _levelItems, _levelTimer, _itemCounter, _hintButton);
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