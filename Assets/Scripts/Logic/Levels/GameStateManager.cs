using Services.StateMachine;
using Services.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Logic.Levels
{
    public class GameStateManager : MonoBehaviour
    {
        private GameStateMachine _stateMachine;
        private LevelItems _levelItems;

        [Inject]
        private void Construct(GameStateMachine stateMachine, LevelItems levelItems)
        {
            _stateMachine = stateMachine;
            _levelItems = levelItems;
        }

        private void Awake()
        {
            _stateMachine.AddState(new InitialState(_stateMachine, _levelItems)); 
            _stateMachine.AddState(new TrainingState(_stateMachine));
        }

        private void Start() =>
            _stateMachine.Enter<InitialState>();
    }
}