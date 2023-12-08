using System.Collections;
using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.Localization.Settings;
using Zenject;

namespace Services.Localization
{
    public class LocalizationService : MonoBehaviour, ILocalizationService
    {
        private bool _active;
        
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        public void SetLocale(int localeID) =>
            _ = StartCoroutine(ChangeLocaleCoroutine(localeID));

        public void ChangeLocale()
        {
            if (_active) return;
            
            int localeID = _progressService.GetUserProgress.Locale == 0 ? 1 : 0;
            _ = StartCoroutine(ChangeLocaleCoroutine(localeID));
        }

        private IEnumerator ChangeLocaleCoroutine(int localeID)
        {
            _active = true;
            yield return LocalizationSettings.InitializationOperation;
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[localeID];
            _progressService.GetUserProgress.Locale = localeID;
            _saveLoadService.SaveProgress();
            _active = false;
        }
    }
}