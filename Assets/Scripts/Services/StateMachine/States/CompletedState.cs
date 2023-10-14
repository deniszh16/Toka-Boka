using Logic.Levels;
using Services.PersistentProgress;
using Services.SaveLoad;

namespace Services.StateMachine.States
{
    public class CompletedState : BaseStates
    {
        private readonly LevelScore _levelScore;
        private readonly LevelResults _levelResults;
        private readonly IPersistentProgressService _progressService;
        private readonly ISaveLoadService _saveLoadService;
        
        public CompletedState(GameStateMachine stateMachine, LevelScore levelScore, LevelResults levelResults,
            IPersistentProgressService progressService, ISaveLoadService saveLoadService) : base(stateMachine)
        {
            _levelScore = levelScore;
            _levelResults = levelResults;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public override void Enter()
        {
            _levelResults.ShowVictoryPanel();
            _levelResults.ShowCurrentScore(_levelScore.Score);
            _progressService.UserProgress.Hearts += _levelScore.Score;
            _saveLoadService.SaveProgress();
        }

        public override void Exit()
        {
        }
    }
}