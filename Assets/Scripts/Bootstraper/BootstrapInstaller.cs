using Services.PersistentProgress;
using Services.YandexService;
using Services.Localization;
using Services.SceneLoader;
using Services.SaveLoad;
using Services.Sound;
using UnityEngine;
using Zenject;

namespace Bootstraper
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private SceneLoaderService _sceneLoader;
        [SerializeField] private SoundService _soundService;
        [SerializeField] private LocalizationService _localizationService;
        [SerializeField] private YandexService _yandexService;

        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        
        public override void InstallBindings()
        {
            BindPersistentProgress();
            BindSaveLoadService();
            BindLocalizationService();
            BindSceneLoader();
            BindSoundService();
            BindYandexService();
        }
        
        private void BindPersistentProgress()
        {
            _progressService = new PersistentProgressService();
            Container.BindInstance(_progressService).AsSingle();
        }

        private void BindSaveLoadService()
        {
            _saveLoadService = new SaveLoadService(_progressService);
            Container.BindInstance(_saveLoadService).AsSingle();
        }

        private void BindLocalizationService()
        {
            LocalizationService localizationService =
                Container.InstantiatePrefabForComponent<LocalizationService>(_localizationService);
            Container.Bind<ILocalizationService>().To<LocalizationService>().FromInstance(localizationService).AsSingle();
        }

        private void BindSceneLoader()
        {
            SceneLoaderService sceneLoader = Container.InstantiatePrefabForComponent<SceneLoaderService>(_sceneLoader);
            Container.Bind<ISceneLoaderService>().To<SceneLoaderService>().FromInstance(sceneLoader).AsSingle();
        }

        private void BindSoundService()
        {
            SoundService soundService = Container.InstantiatePrefabForComponent<SoundService>(_soundService);
            Container.Bind<ISoundService>().To<SoundService>().FromInstance(soundService).AsSingle();
        }

        private void BindYandexService()
        {
            YandexService yandexService = Container.InstantiatePrefabForComponent<YandexService>(_yandexService);
            Container.Bind<IYandexService>().To<YandexService>().FromInstance(yandexService).AsSingle();
        }
    }
}