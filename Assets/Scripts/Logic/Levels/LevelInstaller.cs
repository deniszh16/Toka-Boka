using Services.StateMachine;
using UnityEngine;
using Zenject;

namespace Logic.Levels
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelItems _levelItems;
        [SerializeField] private TrainingPanel _trainingPanel;
        [SerializeField] private CameraMove _movingCamera;
        [SerializeField] private SearchItem _searchItem;
        [SerializeField] private Timer _timer;
        [SerializeField] private GamePause _gamePause;
        [SerializeField] private ItemSelection _itemSelection;
        
        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindLevelItems();
            BindTrainingPanel();
            BindCameraMove();
            BindSearchItem();
            BindTimer();
            BindGamePause();
            BindItemSelection();
        }

        private void BindGameStateMachine()
        {
            GameStateMachine gameStateMachine = new GameStateMachine();
            Container.BindInstance(gameStateMachine).AsSingle();
        }

        private void BindLevelItems() =>
            Container.BindInstance(_levelItems).AsSingle();

        private void BindTrainingPanel() =>
            Container.BindInstance(_trainingPanel).AsSingle();

        private void BindCameraMove() =>
            Container.BindInstance(_movingCamera).AsSingle();

        private void BindSearchItem() =>
            Container.BindInstance(_searchItem).AsSingle();

        private void BindTimer() =>
            Container.BindInstance(_timer).AsSingle();

        private void BindGamePause() =>
            Container.BindInstance(_gamePause).AsSingle();

        private void BindItemSelection() =>
            Container.BindInstance(_itemSelection).AsSingle();
    }
}