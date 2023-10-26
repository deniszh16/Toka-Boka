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
            _progressService.UserProgress.Hearts += _levelScore.Score;

            if (_progressService.UserProgress.Progress <= _currentLevel.LevelNumber)
                _progressService.UserProgress.Progress++;
            
            _saveLoadService.SaveProgress();
        }

        public override void Exit()
        {
        }
    }
}