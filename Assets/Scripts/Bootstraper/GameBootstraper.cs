using Services.PersistentProgress;
using Services.Localization;
using Services.SceneLoader;
using Services.SaveLoad;
using UnityEngine;
using Zenject;
using Data;

namespace Bootstraper
{
    public class GameBootstraper : MonoBehaviour
    {
        private ISceneLoaderService _sceneLoaderService;
        private ILocalizationService _localizationService;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService, ILocalizationService localizationService,
            IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _sceneLoaderService = sceneLoaderService;
            _localizationService = localizationService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void Awake() =>
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

        private void Start()
        {
            LoadProgressOrInitNew();
            _localizationService.SetLocale(_progressService.UserProgress.Locale);
            _sceneLoaderService.LoadSceneAsync(Scenes.MainMenu, screensaver: false, delay: 1.5f);
        }

        private void LoadProgressOrInitNew()
        {
            _progressService.UserProgress =
                _saveLoadService.LoadProgress() ?? new UserProgress();
        }
    }
}