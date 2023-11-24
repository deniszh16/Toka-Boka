using Logic.Levels;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using YG;

namespace Logic.UI.Buttons
{
    public class DoubleRewardButton : MonoBehaviour
    {
        [Header("Компонент кнопки")]
        [SerializeField] private Button _button;
        
        private const int RewardId = 1;

        private LevelScore _levelScore;
        private LevelResults _levelResults;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(LevelScore levelScore, LevelResults levelResults,
            IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _levelScore = levelScore;
            _levelResults = levelResults;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(() => ShowVideoAds(id: 1));
            YandexGame.RewardVideoEvent += DoubleCurrentScore;
        }

        private void ShowVideoAds(int id) =>
            YandexGame.RewVideoShow(id);

        private void DoubleCurrentScore(int id)
        {
            if (id == RewardId)
            {
                _progressService.UserProgress.Hearts += _levelScore.Score;
                _saveLoadService.SaveProgress();
                _levelResults.ShowCurrentScore(_levelScore.Score * 2);
                _button.interactable = false;
            }
        }

        private void OnDisable()
        {
            _button.onClick.RemoveAllListeners();
            YandexGame.RewardVideoEvent -= DoubleCurrentScore;
        }
    }
}