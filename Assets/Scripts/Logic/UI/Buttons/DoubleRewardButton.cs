using Logic.Levels;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class DoubleRewardButton : MonoBehaviour
    {
        private Button _button;
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

        private void Awake() =>
            _button = GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(DoubleCurrentScore);

        private void DoubleCurrentScore()
        {
            _progressService.UserProgress.Hearts += _levelScore.Score;
            _saveLoadService.SaveProgress();
            _levelResults.ShowCurrentScore(_levelScore.Score * 2);
            _button.interactable = false;
        }

        private void OnDisable() =>
            _button.onClick.RemoveListener(DoubleCurrentScore);
    }
}