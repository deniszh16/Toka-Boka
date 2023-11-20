using Services.Localization;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class LanguageButton : MonoBehaviour
    {
        private Button _button;
        private ILocalizationService _localizationService;

        [Inject]
        private void Construct(ILocalizationService localizationService) =>
            _localizationService = localizationService;

        private void Awake() =>
            _button = GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(_localizationService.ChangeLocale);

        private void OnDisable() =>
            _button.onClick.RemoveListener(_localizationService.ChangeLocale);
    }
}