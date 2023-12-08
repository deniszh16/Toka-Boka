using Logic.Levels;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;

namespace Services.StateMachine.States
{
    public class CompletedState : BaseStates
    {
        private readonly LevelScore _levelScore;
        private readonly CurrentLevel _currentLevel;
        private readonly GamePause _gamePause;
        private readonly LevelResults _levelResults;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;

        public CompletedState(GameStateMachine stateMachine, LevelScore levelScore, CurrentLevel currentLevel, GamePause gamePause,
            LevelResults levelResults, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
            : base(stateMachine)
        {
            _levelScore = levelScore;
            _currentLevel = currentLevel;
            _gamePause = gamePause;
            _levelResults = levelResults;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public override void Enter()
        {
            _gamePause.ChangeButtonVisibility(visibility: false);
            _levelResults.ShowVictoryPanel(Camera.main.transform.position);
            _levelResults.ShowCurrentScore(_levelScore.Score);
            _progressService.GetUserProgress.Hearts += _levelScore.Score;
            _progressService.GetUserProgress.ChangeAttempts(levelNumber: _currentLevel.LevelNumber - 1);
            _progressService.GetUserProgress.ChangeStars(levelNumber: _currentLevel.LevelNumber - 1, value: 1);

            if (_progressService.GetUserProgress.Progress <= _currentLevel.LevelNumber)
                _progressService.GetUserProgress.Progress++;
            
            _saveLoadService.SaveProgress();
        }

        public override void Exit()
        {
        }
    }
}