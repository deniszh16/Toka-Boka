using Services.StateMachine;
using UnityEngine;
using Zenject;

namespace Logic.Levels
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private LevelItems _levelItems;
        [SerializeField] private CurrentLevel _currentLevel;
        [SerializeField] private TrainingPanel _trainingPanel;
        [SerializeField] private CameraMove _movingCamera;
        [SerializeField] private SearchItem _searchItem;
        [SerializeField] private LevelTimer _levelTimer;
        [SerializeField] private GamePause _gamePause;
        [SerializeField] private ItemSelection _itemSelection;
        [SerializeField] private LevelScore _levelScore;
        [SerializeField] private LevelResults _levelResults;
        
        public override void InstallBindings()
        {
            BindGameStateMachine();
            BindLevelItems();
            BindCurrentLevel();
            BindTrainingPanel();
            BindCameraMove();
            BindSearchItem();
            BindTimer();
            BindGamePause();
            BindItemSelection();
            BindLevelScore();
            BindLevelResults();
        }

        private void BindGameStateMachine()
        {
            GameStateMachine gameStateMachine = new GameStateMachine();
            Container.BindInstance(gameStateMachine).AsSingle();
        }

        private void BindLevelItems() =>
            Container.BindInstance(_levelItems).AsSingle();
        
        private void BindCurrentLevel() =>
            Container.BindInstance(_currentLevel).AsSingle();

        private void BindTrainingPanel() =>
            Container.BindInstance(_trainingPanel).AsSingle();

        private void BindCameraMove() =>
            Container.BindInstance(_movingCamera).AsSingle();

        private void BindSearchItem() =>
            Container.BindInstance(_searchItem).AsSingle();

        private void BindTimer() =>
            Container.BindInstance(_levelTimer).AsSingle();

        private void BindGamePause() =>
            Container.BindInstance(_gamePause).AsSingle();

        private void BindItemSelection() =>
            Container.BindInstance(_itemSelection).AsSingle();
        
        private void BindLevelScore() =>
            Container.BindInstance(_levelScore).AsSingle();

        private void BindLevelResults() =>
            Container.BindInstance(_levelResults).AsSingle();
    }
}