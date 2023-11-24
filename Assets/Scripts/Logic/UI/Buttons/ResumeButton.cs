using Logic.Levels;
using Services.StateMachine;
using Services.StateMachine.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using YG;

namespace Logic.UI.Buttons
{
    public class ResumeButton : MonoBehaviour
    {
        [Header("Компонент кнопки")]
        [SerializeField] private Button _button;
        
        private const int RewardId = 2;
        
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

        private void OnEnable()
        {
            _button.onClick.AddListener(() => ShowVideoAds(id: 2));
            YandexGame.RewardVideoEvent += ContinueLevel;
        }
        
        private void ShowVideoAds(int id) =>
            YandexGame.RewVideoShow(id);

        private void ContinueLevel(int id)
        {
            if (id == RewardId)
            {
                _levelTimer.SetTimer();
                _stateMachine.Enter<PlayState>();
                _searchItem.ShowCurrentItem();
            }
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
            YandexGame.RewardVideoEvent -= ContinueLevel;
        }
    }
}