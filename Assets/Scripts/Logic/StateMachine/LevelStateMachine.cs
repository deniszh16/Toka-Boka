using Services.PersistentProgress;
using Logic.StateMachine.States;
using Services.UpdateService;
using Services.StateMachine;
using Services.SaveLoad;
using Logic.UI.Buttons;
using Logic.UI.Levels;
using Services.Sound;
using Logic.Levels;
using UnityEngine;
using Zenject;

namespace Logic.StateMachine
{
    public class LevelStateMachine : MonoBehaviour
    {
        private GameStateMachine _stateMachine;
        private IMonoUpdateService _monoUpdateService;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private ISoundService _soundService;
        
        private CurrentLevel _currentLevel;
        private CameraMove _cameraMove;
        private LevelItems _levelItems;
        private SearchItem _searchItem;
        private ItemSelection _itemSelection;
        private LevelTimer _levelTimer;
        
        private TrainingPanel _trainingPanel;
        private ItemCounter _itemCounter;
        private HintButton _hintButton;
        private LevelUI _levelUI;

        [Inject]
        private void Construct(GameStateMachine stateMachine, IMonoUpdateService monoUpdateService, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService, ISoundService soundService, CurrentLevel currentLevel, CameraMove cameraMove, LevelItems levelItems, SearchItem searchItem,
            ItemSelection itemSelection, LevelTimer levelTimer, TrainingPanel trainingPanel, ItemCounter itemCounter, HintButton hintButton, LevelUI levelUI)
        {
            _stateMachine = stateMachine;
            _monoUpdateService = monoUpdateService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _soundService = soundService;
            
            _currentLevel = currentLevel;
            _cameraMove = cameraMove;
            _levelItems = levelItems;
            _searchItem = searchItem;
            _itemSelection = itemSelection;
            _levelTimer = levelTimer;
            
            _trainingPanel = trainingPanel;
            _itemCounter = itemCounter;
            _hintButton = hintButton;
            _levelUI = levelUI;
        }

        private void Awake()
        {
            _stateMachine.AddState(new InitialState(_stateMachine, _progressService, _currentLevel, _levelItems, _searchItem, _levelTimer, _trainingPanel, _itemCounter, _hintButton)); 
            _stateMachine.AddState(new TrainingState(_stateMachine, _trainingPanel));
            _stateMachine.AddState(new PlayState(_stateMachine, _soundService, _monoUpdateService, _cameraMove, _searchItem, _levelTimer, _itemSelection, _levelUI));
            _stateMachine.AddState(new PauseState(_stateMachine, _levelUI));
            _stateMachine.AddState(new LosingState(_stateMachine, _levelUI));
            _stateMachine.AddState(new CompletedState(_stateMachine, _progressService, _saveLoadService, _currentLevel, _levelUI));
        }

        private void Start() =>
            _stateMachine.Enter<InitialState>();
    }
}