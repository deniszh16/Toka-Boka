using Services.StateMachine.States;
using Services.PersistentProgress;
using Services.StateMachine;
using Services.SaveLoad;
using Logic.UI.Levels;
using Logic.Levels;
using UnityEngine;

namespace Logic.StateMachine.States
{
    public class CompletedState : BaseStates
    {
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        
        private readonly CurrentLevel _currentLevel;
        private readonly LevelUI _levelUI;

        public CompletedState(GameStateMachine stateMachine, IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            CurrentLevel currentLevel, LevelUI levelUI) : base(stateMachine)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            
            _currentLevel = currentLevel;
            _levelUI = levelUI;
        }

        public override void Enter()
        {
            _levelUI.ChangeButtonVisibility(visibility: false);
            _levelUI.ShowVictoryPanel(Camera.main.transform.position);
            _levelUI.ShowCurrentScore(_currentLevel.Score);
            
            _progressService.GetUserProgress.Hearts += _currentLevel.Score;
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