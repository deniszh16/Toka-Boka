using Services.PersistentProgress;
using UnityEngine;
using UnityEngine.Localization;
using Zenject;
using TMPro;

namespace Logic.ListOfLevels
{
    public class OpenPets : MonoBehaviour
    {
        [Header("Локализированная строка")]
        [SerializeField] private LocalizedString _localizedString;

        [Header("Текстовый компонент")]
        [SerializeField] private TextMeshProUGUI _quantityText;

        private int _numberOfPets;
        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(IPersistentProgressService progressService) =>
            _progressService = progressService;

        private void OnEnable()
        {
            _localizedString.Arguments = new object[] { _numberOfPets };
            _localizedString.StringChanged += UpdateText;
        }

        private void UpdateText(string value) =>
            _quantityText.text = value;

        public void UpdateNumberOfPets(int levelNumber)
        {
            _numberOfPets = _progressService.GetUserProgress.CharacterPets[levelNumber].OpenPets;
            _localizedString.Arguments[0] = _numberOfPets;
            _localizedString.RefreshString();
        }

        private void OnDisable() =>
            _localizedString.StringChanged -= UpdateText;
    }
}