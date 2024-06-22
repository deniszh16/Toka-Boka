using DZGames.TokaBoka.Services;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace DZGames.TokaBoka.LifetimeScopes
{
    public class GlobalLifetimeScope : LifetimeScope
    {
        [SerializeField] private SceneLoaderService _sceneLoaderService;
        [SerializeField] private SoundService _soundService;
        [SerializeField] private MonoUpdateService _monoUpdateService;
        [SerializeField] private YandexService _yandexService;
        
        protected override void Configure(IContainerBuilder builder)
        {
            BindPersistentProgress(builder);
            BindSaveLoadService(builder);
            BindLocalizationService(builder);
            BindSceneLoader(builder);
            BindSoundService(builder);
            BindMonoUpdateService(builder);
            BindYandexService(builder);
            DontDestroyOnLoad(gameObject);
        }
        
        private void BindPersistentProgress(IContainerBuilder builder) =>
            builder.Register<PersistentProgressService>(Lifetime.Singleton).AsImplementedInterfaces();
        
        private void BindSaveLoadService(IContainerBuilder builder) =>
            builder.Register<SaveLoadService>(Lifetime.Singleton).AsImplementedInterfaces();

        private void BindLocalizationService(IContainerBuilder builder) =>
            builder.Register<LocalizationService>(Lifetime.Singleton).AsImplementedInterfaces();
        
        private void BindSceneLoader(IContainerBuilder builder) =>
            builder.RegisterComponent(_sceneLoaderService).AsImplementedInterfaces();
        
        private void BindSoundService(IContainerBuilder builder) =>
            builder.RegisterComponent(_soundService).AsImplementedInterfaces();

        private void BindMonoUpdateService(IContainerBuilder builder) =>
            builder.RegisterComponent(_monoUpdateService).AsImplementedInterfaces();
        
        private void BindYandexService(IContainerBuilder builder)
        {
            builder.RegisterComponent(_yandexService).AsImplementedInterfaces();
            _yandexService.Init();
        }
    }
}