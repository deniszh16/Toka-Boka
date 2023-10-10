﻿using Services.StateMachine;
using Services.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.Levels
{
    public class GameStateManager : MonoBehaviour
    {
        private GameStateMachine _stateMachine;
        private LevelItems _levelItems;
        private TrainingPanel _trainingPanel;
        private CameraMove _cameraMove;
        private SearchItem _searchItem;
        private Timer _timer;
        private ItemSelection _itemSelection;
        private GamePause _gamePause;

        [Inject]
        private void Construct(GameStateMachine stateMachine, LevelItems levelItems, TrainingPanel trainingPanel,
            CameraMove cameraMove, SearchItem searchItem, Timer timer, ItemSelection itemSelection, GamePause gamePause)
        {
            _stateMachine = stateMachine;
            _levelItems = levelItems;
            _trainingPanel = trainingPanel;
            _cameraMove = cameraMove;
            _searchItem = searchItem;
            _timer = timer;
            _itemSelection = itemSelection;
            _gamePause = gamePause;
        }

        private void Awake()
        {
            _stateMachine.AddState(new InitialState(_stateMachine, _levelItems, _trainingPanel, _searchItem)); 
            _stateMachine.AddState(new TrainingState(_stateMachine, _trainingPanel));
            _stateMachine.AddState(new PlayState(_stateMachine, _cameraMove, _searchItem, _timer, _itemSelection, _gamePause));
            _stateMachine.AddState(new PauseState(_stateMachine, _gamePause));
            _stateMachine.AddState(new LosingState(_stateMachine));
            _stateMachine.AddState(new CompletedState(_stateMachine));
        }

        private void Start() =>
            _stateMachine.Enter<InitialState>();
    }
}