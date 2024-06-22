using DZGames.TokaBoka.Levels;
using DZGames.TokaBoka.Services;
using DZGames.TokaBoka.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DZGames.TokaBoka.LifetimeScopes
{
    public class CurrentLevelLifetimeScope : LifetimeScope
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
        
        protected override void Configure(IContainerBuilder builder)
        {
            BindGameStateMachine(builder);

            BindCurrentLevel(builder);
            BindCameraMove(builder);
            BindLevelItems(builder);
            BindSearchItem(builder);
            BindItemSelection(builder);
            BindLevelTimer(builder);

            BindTrainingPanel(builder);
            BindItemCounter(builder);
            BindHintButton(builder);
            BindLevelUI(builder);
        }
        
        private void BindGameStateMachine(IContainerBuilder builder) =>
            builder.Register<GameStateMachine>(Lifetime.Singleton);

        private void BindCurrentLevel(IContainerBuilder builder) =>
            builder.RegisterComponent(_currentLevel);

        private void BindCameraMove(IContainerBuilder builder) =>
            builder.RegisterComponent(_movingCamera);

        private void BindLevelItems(IContainerBuilder builder) =>
            builder.RegisterComponent(_levelItems);

        private void BindSearchItem(IContainerBuilder builder) =>
            builder.RegisterComponent(_searchItem);

        private void BindItemSelection(IContainerBuilder builder) =>
            builder.RegisterComponent(_itemSelection);

        private void BindLevelTimer(IContainerBuilder builder) =>
            builder.RegisterComponent(_levelTimer);

        private void BindTrainingPanel(IContainerBuilder builder) =>
            builder.RegisterComponent(_trainingPanel);

        private void BindItemCounter(IContainerBuilder builder) =>
            builder.RegisterComponent(_itemCounter);

        private void BindHintButton(IContainerBuilder builder) =>
            builder.RegisterComponent(_hintButton);

        private void BindLevelUI(IContainerBuilder builder) =>
            builder.RegisterComponent(_levelUI);
    }
}