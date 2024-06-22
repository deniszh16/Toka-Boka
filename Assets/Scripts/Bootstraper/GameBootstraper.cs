using System.Threading;
using Cysharp.Threading.Tasks;
using TokaBoka.Data;
using TokaBoka.Services;
using UnityEngine;
using VContainer;

namespace TokaBoka.Bootstrapper
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

        private void Awake()
        {
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Application.targetFrameRate = 60;
        }

        private void Start()
        {
            LoadProgressOrInitNew();
            _localizationService.SetLocale(_progressService.GetUserProgress.SettingsData.Locale);
            _soundService.SoundActivity = _progressService.GetUserProgress.SettingsData.Sound;
            _sceneLoaderService.LoadSceneAsync((int)Scenes.MainMenu, screensaver: false, delay: 1f, CancellationToken.None).Forget();
        }

        private void LoadProgressOrInitNew() =>
            _progressService.SetUserProgress(_saveLoadService.LoadProgress() ?? new UserProgress());
    }
}