using TokaBoka.Levels;
using TokaBoka.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TokaBoka.UI
{
    public class DoubleRewardButton : MonoBehaviour
    {
        [Header("Компонент кнопки")]
        [SerializeField] private Button _button;
        
        private const int RewardId = 1;

        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private IYandexService _yandexService;
        
        private CurrentLevel _currentLevel;
        private LevelUI _levelUI;
        
        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            IYandexService yandexService, CurrentLevel currentLevel, LevelUI levelUI)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _yandexService = yandexService;
            
            _currentLevel = currentLevel;
            _levelUI = levelUI;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(() => _yandexService.ShowRewardedAds(id: 1));
            _yandexService.AdsViewed += DoubleCurrentScore;
        }

        private void DoubleCurrentScore(int id)
        {
            if (id == RewardId)
            {
                _progressService.GetUserProgress.Hearts += _currentLevel.Score;
                _saveLoadService.SaveProgress();
                _levelUI.ShowCurrentScore(_currentLevel.Score * 2);
                _button.interactable = false;
            }
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
            _yandexService.AdsViewed -= DoubleCurrentScore;
        }
    }
}