using Logic.Levels;

namespace Services.StateMachine.States
{
    public class PlayState : BaseStates
    {
        private readonly CameraMove _cameraMove;
        private readonly SearchItem _searchItem;
        private readonly LevelTimer _timer;
        private readonly ItemSelection _itemSelection;
        
        private readonly GamePause _gamePause;

        public PlayState(GameStateMachine stateMachine, CameraMove cameraMove, SearchItem searchItem,
            LevelTimer timer, ItemSelection itemSelection, GamePause gamePause) : base(stateMachine)
        {
            _cameraMove = cameraMove;
            _searchItem = searchItem;
            _timer = timer;
            _itemSelection = itemSelection;
            _gamePause = gamePause;
        }

        public override void Enter()
        {
            _cameraMove.Activity = true;
            _searchItem.IconContainer.SetActive(true);
            _searchItem.AllItemsFound += OnLevelPassed;
            _timer.TimerCompleted += OnLevelLost;
            _timer.ChangeTimerActivity(value: true);
            _itemSelection.ChangeActivity(activity: true);
            _gamePause.ChangeButtonVisibility(visibility: true);
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
            _timer.TimerCompleted -= OnLevelLost;
            _timer.ChangeTimerActivity(value: false);
            _itemSelection.ChangeActivity(activity: false);
        }
    }
}