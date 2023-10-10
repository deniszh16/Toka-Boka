using Logic.Levels;

namespace Services.StateMachine.States
{
    public class PlayState : BaseStates
    {
        private readonly CameraMove _cameraMove;
        private readonly SearchItem _searchItem;
        private readonly Timer _timer;
        private readonly ItemSelection _itemSelection;
        private readonly GamePause _gamePause;

        public PlayState(GameStateMachine stateMachine, CameraMove cameraMove, SearchItem searchItem,
            Timer timer, ItemSelection itemSelection, GamePause gamePause) : base(stateMachine)
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
            _timer.TimerCompleted += LevelLost;
            _timer.ChangeTimerActivity(value: true);
            _itemSelection.ChangeActivity(activity: true);
            _gamePause.ChangeButtonVisibility(visibility: true);
        }

        private void LevelLost() =>
            _stateMachine.Enter<LosingState>();

        public override void Exit()
        {
            _cameraMove.Activity = false;
            _searchItem.IconContainer.SetActive(false);
            _timer.ChangeTimerActivity(value: false);
            _timer.TimerCompleted -= LevelLost;
            _itemSelection.ChangeActivity(activity: false);
        }
    }
}