using Logic.Levels;

namespace Services.StateMachine.States
{
    public class PlayState : BaseStates
    {
        private readonly CameraMove _cameraMove;
        private readonly SearchItem _searchItem;
        private readonly Timer _timer;
        
        public PlayState(GameStateMachine stateMachine, CameraMove cameraMove, SearchItem searchItem,
            Timer timer) : base(stateMachine)
        {
            _cameraMove = cameraMove;
            _searchItem = searchItem;
            _timer = timer;
        }

        public override void Enter()
        {
            _cameraMove.Activity = true;
            _searchItem.IconContainer.SetActive(true);
            _searchItem.ShowCurrentItem();
            _timer.TimerCompleted += LevelLost;
        }

        private void LevelLost() =>
            _stateMachine.Enter<LosingState>();

        public override void Exit()
        {
            _cameraMove.Activity = false;
            _searchItem.IconContainer.SetActive(false);
            _timer.TimerCompleted -= LevelLost;
        }
    }
}