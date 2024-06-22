using TokaBoka.Levels;
using TokaBoka.Services;
using TokaBoka.UI;

namespace TokaBoka.StateMachine
{
    public class PlayState : BaseStates
    {
        private readonly ISoundService _soundService;
        private readonly IMonoUpdateService _monoUpdateService;
        
        private readonly CameraMove _cameraMove;
        private readonly SearchItem _searchItem;
        private readonly ItemSelection _itemSelection;
        private readonly LevelTimer _timer;

        private readonly LevelUI _levelUI;

        public PlayState(GameStateMachine stateMachine, ISoundService soundService, IMonoUpdateService monoUpdateService, CameraMove cameraMove,
            SearchItem searchItem, LevelTimer timer, ItemSelection itemSelection, LevelUI levelUI) : base(stateMachine)
        {
            _soundService = soundService;
            _monoUpdateService = monoUpdateService;
            
            _cameraMove = cameraMove;
            _searchItem = searchItem;
            _itemSelection = itemSelection;
            _timer = timer;
            
            _levelUI = levelUI;
        }

        public override void Enter()
        {
            _cameraMove.Activity = true;
            _cameraMove.Init(_monoUpdateService);
            
            _searchItem.IconContainer.SetActive(true);
            _searchItem.AllItemsFound += OnLevelPassed;
            _timer.TimerCompleted += OnLevelLost;
            _timer.StartTimer();
            
            _itemSelection.Init(_soundService, _monoUpdateService, _searchItem);
            _itemSelection.ChangeActivity(activity: true);

            _levelUI.ChangeButtonVisibility(visibility: true);
            _soundService.SettingBackgroundMusic();
        }

        private void OnLevelPassed() =>
            _stateMachine.Enter<CompletedState>();

        private void OnLevelLost() =>
            _stateMachine.Enter<LosingState>();

        public override void Exit()
        {
            _cameraMove.Activity = false;
            _searchItem.IconContainer.SetActive(false);
            _searchItem.AllItemsFound -= OnLevelPassed;
            _timer.StopTimer();
            _timer.TimerCompleted -= OnLevelLost;
            
            _itemSelection.ChangeActivity(activity: false);
            _soundService.StopBackgroundMusic();
        }
    }
}