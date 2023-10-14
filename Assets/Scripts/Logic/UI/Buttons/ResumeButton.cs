using Logic.Levels;
using Services.StateMachine;
using Services.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class ResumeButton : MonoBehaviour
    {
        private Button _button;
        private GameStateMachine _stateMachine;
        private SearchItem _searchItem;
        private LevelTimer _levelTimer;
        
        [Inject]
        private void Construct(GameStateMachine stateMachine, SearchItem searchItem, LevelTimer levelTimer)
        {
            _stateMachine = stateMachine;
            _searchItem = searchItem;
            _levelTimer = levelTimer;
        }

        private void Awake() =>
            _button = GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(ContinueLevel);

        private void ContinueLevel()
        {
            _levelTimer.SetTimer();
            _stateMachine.Enter<PlayState>();
            _searchItem.ShowCurrentItem();
        }

        private void OnDisable() =>
            _button.onClick.RemoveListener(ContinueLevel);
    }
}