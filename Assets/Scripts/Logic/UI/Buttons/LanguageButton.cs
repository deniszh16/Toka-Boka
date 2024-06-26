﻿using TokaBoka.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TokaBoka.UI
{
    public class LanguageButton : MonoBehaviour
    {
        [Header("Компонент кнопки")]
        [SerializeField] private Button _button;
        
        private ILocalizationService _localizationService;

        [Inject]
        private void Construct(ILocalizationService localizationService) =>
            _localizationService = localizationService;

        private void OnEnable() =>
            _button.onClick.AddListener(_localizationService.ChangeLocale);

        private void OnDisable() =>
            _button.onClick.RemoveListener(_localizationService.ChangeLocale);
    }
}