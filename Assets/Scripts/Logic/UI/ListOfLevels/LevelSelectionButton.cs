﻿using Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI.ListOfLevels
{
    [RequireComponent(typeof(Button), typeof(Image))]
    public class LevelSelectionButton : MonoBehaviour
    {
        [Header("Номер кнопки")]
        [SerializeField] private int _number;

        [Header("Элементы кнопки")]
        [SerializeField] private GameObject _iconLock;
        [SerializeField] private GameObject _textNumber;
        [SerializeField] private GameObject _iconComplete;
        
        [Header("Спрайты кнопки")]
        [SerializeField] private Sprite _levelOpen;
        [SerializeField] private Sprite _levelPassed;
        
        private Button _button;
        private Image _image;

        private IPersistentProgressService _progressService;
        private LevelSelection _levelSelection;

        [Inject]
        private void Construct(IPersistentProgressService progressService, LevelSelection levelSelection)
        {
            _progressService = progressService;
            _levelSelection = levelSelection;
        }

        private void Awake()
        {
            _button = GetComponent<Button>();
            _image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            if (_number == _progressService.UserProgress.Progress)
                CustomizeButton(_textNumber, _levelOpen);
            else if (_number < _progressService.UserProgress.Progress)
                CustomizeButton(_iconComplete, _levelPassed);
        }

        private void CustomizeButton(GameObject buttonElement, Sprite sprite)
        {
            _button.onClick.AddListener(SelectLevel);
            _button.interactable = true;

            buttonElement.SetActive(true);
            _iconLock.SetActive(false);
            _image.sprite = sprite;
        }

        private void SelectLevel() =>
            _levelSelection.SelectLevel(_number);
        
        private void OnDisable() =>
            _button.onClick.RemoveListener(SelectLevel);
    }
}