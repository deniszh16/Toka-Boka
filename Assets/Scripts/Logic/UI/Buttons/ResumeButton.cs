using TokaBoka.Levels;
using TokaBoka.Services;
using TokaBoka.StateMachine;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TokaBoka.UI
{
    public class ResumeButton : MonoBehaviour
    {
        [Header("Компонент кнопки")]
        [SerializeField] private Button _button;
        
        private const int RewardId = 2;
        
        private GameStateMachine _stateMachine;
        private IYandexService _yandexService;
        
        private SearchItem _searchItem;
        private LevelTimer _levelTimer;
        
        [Inject]
        private void Construct(GameStateMachine stateMachine, IYandexService yandexService,
            SearchItem searchItem, LevelTimer levelTimer)
        {
            _stateMachine = stateMachine;
            _yandexService = yandexService;
            
            _searchItem = searchItem;
            _levelTimer = levelTimer;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(() => _yandexService.ShowRewardedAds(id: 2));
            _yandexService.AdsViewed += ContinueLevel;
        }

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
            _yandexService.AdsViewed -= ContinueLevel;
        }
    }
}