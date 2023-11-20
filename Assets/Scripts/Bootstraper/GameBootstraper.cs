using Services.PersistentProgress;
using Services.Localization;
using Services.SceneLoader;
using Services.SaveLoad;
using Services.Sound;
using UnityEngine;
using Zenject;
using Data;

namespace Bootstraper
{
    public class GameBootstraper : MonoBehaviour
    {
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private ILocalizationService _localizationService;
        private ISceneLoaderService _sceneLoaderService;
        private ISoundService _soundService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService, 
            ILocalizationService localizationService, ISceneLoaderService sceneLoaderService, ISoundService soundService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _localizationService = localizationService;
            _sceneLoaderService = sceneLoaderService;
            _soundService = soundService;
        }

        private void Awake() =>
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

        private void Start()
        {
            LoadProgressOrInitNew();
            _localizationService.SetLocale(_progressService.UserProgress.Locale);
            _soundService.SoundActivity = _progressService.UserProgress.Sound;
            _sceneLoaderService.LoadSceneAsync(Scenes.MainMenu, screensaver: false, delay: 1.5f);
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.UserProgress =
                _saveLoadService.LoadProgress() ?? new UserProgress();
        }
    }
}