using Services.StateMachine;
using Services.StateMachine.States;
using UnityEngine;
using Zenject;

namespace Logic.Levels
{
    public class GamePause : MonoBehaviour
    {
        [Header("Кнопка паузы")]
        [SerializeField] private GameObject _pauseButton;
        
        [Header("Панель паузы")]
        [SerializeField] private GameObject _pausePanel;
        
        private GameStateMachine _gameStateMachine;
        
        [Inject]
        private void Construct(GameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;
        
        public void TogglePause(bool pause)
        {
            if (pause) _gameStateMachine.Enter<PauseState>();
            else _gameStateMachine.Enter<PlayState>();
        }
        
        public void ChangeButtonVisibility(bool visibility) =>
            _pauseButton.SetActive(visibility);

        public void ChangePausePanelVisibility(bool visibility) =>
            _pausePanel.SetActive(visibility);
    }
}