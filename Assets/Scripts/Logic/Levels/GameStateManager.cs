using Services.PersistentProgress;
using Services.SaveLoad;
using Services.StateMachine;
using Services.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Logic.Levels
{
    public class GameStateManager : MonoBehaviour
    {
        private GameStateMachine _stateMachine;
        private TrainingPanel _trainingPanel;
        
        private LevelItems _levelItems;
        private CameraMove _cameraMove;
        private SearchItem _searchItem;
        private LevelTimer _levelTimer;
        private ItemSelection _itemSelection;
        
        private GamePause _gamePause;
        private LevelScore _levelScore;
        private CurrentLevel _currentLevel;
        private LevelResults _levelResults;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(GameStateMachine stateMachine, LevelItems levelItems, TrainingPanel trainingPanel, CameraMove cameraMove,
            SearchItem searchItem, LevelTimer levelTimer, ItemSelection itemSelection, GamePause gamePause, LevelScore levelScore,
            CurrentLevel currentLevel, LevelResults levelResults, IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _trainingPanel = trainingPanel;

            _levelItems = levelItems;
            _cameraMove = cameraMove;
            _searchItem = searchItem;
            _levelTimer = levelTimer;
            _itemSelection = itemSelection;
            
            _gamePause = gamePause;
            _levelScore = levelScore;
            _currentLevel = currentLevel;
            _levelResults = levelResults;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void Awake()
        {
            _stateMachine.AddState(new InitialState(_stateMachine, _levelItems, _trainingPanel, _searchItem)); 
            _stateMachine.AddState(new TrainingState(_stateMachine, _trainingPanel));
            _stateMachine.AddState(new PlayState(_stateMachine, _cameraMove, _searchItem, _levelTimer, _itemSelection, _gamePause));
            _stateMachine.AddState(new PauseState(_stateMachine, _gamePause));
            _stateMachine.AddState(new LosingState(_stateMachine, _gamePause, _levelResults));
            _stateMachine.AddState(new CompletedState(_stateMachine, _levelScore, _currentLevel, _gamePause, _levelResults, _progressService, _saveLoadService));
        }

        private void Start() =>
            _stateMachine.Enter<InitialState>();
    }
}