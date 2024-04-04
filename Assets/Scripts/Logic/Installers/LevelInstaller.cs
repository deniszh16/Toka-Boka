using Services.StateMachine;
using Logic.UI.Buttons;
using Logic.UI.Levels;
using Logic.Levels;
using UnityEngine;
using Zenject;

namespace Logic.Installers
{
    public class LevelInstaller : MonoInstaller
    {
        [Header("Основные компоненты")]
        [SerializeField] private CurrentLevel _currentLevel;
        [SerializeField] private CameraMove _movingCamera;
        [SerializeField] private LevelItems _levelItems;
        [SerializeField] private SearchItem _searchItem;
        [SerializeField] private ItemSelection _itemSelection;
        [SerializeField] private LevelTimer _levelTimer;
        
        [Header("UI компоненты")]
        [SerializeField] private TrainingPanel _trainingPanel;
        [SerializeField] private ItemCounter _itemCounter;
        [SerializeField] private HintButton _hintButton;
        [SerializeField] private LevelUI _levelUI;
        
        public override void InstallBindings()
        {
            BindGameStateMachine();

            BindCurrentLevel();
            BindCameraMove();
            BindLevelItems();
            BindSearchItem();
            BindItemSelection();
            BindLevelTimer();
            
            BindTrainingPanel();
            BindItemCounter();
            BindHintButton();
            BindLevelUI();
        }

        private void BindGameStateMachine()
        {
            GameStateMachine gameStateMachine = new GameStateMachine();
            Container.BindInstance(gameStateMachine).AsSingle();
        }

        private void BindCurrentLevel() =>
            Container.BindInstance(_currentLevel).AsSingle();
        
        private void BindCameraMove() =>
            Container.BindInstance(_movingCamera).AsSingle();
        
        private void BindLevelItems() =>
            Container.BindInstance(_levelItems).AsSingle();
        
        private void BindSearchItem() =>
            Container.BindInstance(_searchItem).AsSingle();
        
        private void BindItemSelection() =>
            Container.BindInstance(_itemSelection).AsSingle();
        
        private void BindLevelTimer() =>
            Container.BindInstance(_levelTimer).AsSingle();
        
        private void BindTrainingPanel() =>
            Container.BindInstance(_trainingPanel).AsSingle();
        
        private void BindItemCounter() =>
            Container.BindInstance(_itemCounter).AsSingle();
        
        private void BindHintButton() =>
            Container.BindInstance(_hintButton).AsSingle();
        
        private void BindLevelUI() =>
            Container.BindInstance(_levelUI).AsSingle();
    }
}