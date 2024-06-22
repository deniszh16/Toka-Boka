using System;
using UnityEngine;
using UnityEngine.Localization.Components;

namespace DZGames.TokaBoka.UI
{
    public class TrainingPanel : MonoBehaviour
    {
        [Header("Обучение на уровне")]
        [SerializeField] private bool _training;
        
        [Header("Панель обучения")]
        [SerializeField] private GameObject _container;

        [Header("Компонент перевода")]
        [SerializeField] private LocalizeStringEvent _localizeStringEvent;
        
        [Header("Текстовые ключи")]
        [SerializeField] private string[] _textKeys;

        public bool Training => _training;
        
        private int _trainingStage;
        private const string LocalizedTable = "UI Text";

        public event Action TrainingCompleted;

        public void ChangePanelVisibility(bool activity) =>
            _container.SetActive(activity);

        public void ChangeTrainingStage()
        {
            if (_trainingStage < _textKeys.Length - 1)
            {
                _trainingStage++;
                ChangeTranslationKey();
            }
            else
            {
                TrainingCompleted?.Invoke();
            }
        }

        private void ChangeTranslationKey() =>
            _localizeStringEvent.StringReference.SetReference(LocalizedTable, _textKeys[_trainingStage]);
    }
}