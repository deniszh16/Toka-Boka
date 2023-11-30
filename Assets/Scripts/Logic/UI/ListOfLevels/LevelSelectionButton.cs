using Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI.ListOfLevels
{
    public class LevelSelectionButton : MonoBehaviour
    {
        [Header("Номер кнопки")]
        [SerializeField] private int _number;
        
        [Header("Компоненты кнопки")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;

        [Header("Элементы кнопки")]
        [SerializeField] private GameObject _iconLock;
        [SerializeField] private GameObject _textNumber;
        [SerializeField] private GameObject _iconComplete;
        [SerializeField] private GameObject _starsIcon;
        [SerializeField] private NumberOfStars _numberOfStars;
        
        [Header("Спрайты кнопки")]
        [SerializeField] private Sprite _levelOpen;
        [SerializeField] private Sprite _levelPassed;
        
        private IPersistentProgressService _progressService;
        private LevelSelection _levelSelection;

        [Inject]
        private void Construct(IPersistentProgressService progressService, LevelSelection levelSelection)
        {
            _progressService = progressService;
            _levelSelection = levelSelection;
        }

        private void OnEnable()
        {
            if (_number == _progressService.UserProgress.Progress)
            {
                CustomizeButton(_textNumber, _levelOpen);
                
                ShowNumberOfStars();
                _progressService.UserProgress.StarsChanged += ShowNumberOfStars;
            }
            else if (_number < _progressService.UserProgress.Progress)
            {
                CustomizeButton(_iconComplete, _levelPassed);
                
                ShowNumberOfStars();
                _progressService.UserProgress.StarsChanged += ShowNumberOfStars;
            }
        }

        private void CustomizeButton(GameObject buttonElement, Sprite sprite)
        {
            _button.onClick.AddListener(SelectLevel);
            _button.interactable = _number < 8;

            buttonElement.SetActive(true);
            _iconLock.SetActive(false);
            _starsIcon.SetActive(true);
            _image.sprite = sprite;
        }

        private void SelectLevel() =>
            _levelSelection.SelectLevel(_number);

        private void ShowNumberOfStars()
        {
            int stars = _progressService.UserProgress.GetNumberOfStars(_number - 1);
            _numberOfStars.ShowNumberOfStars(stars);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SelectLevel);
            _progressService.UserProgress.StarsChanged -= ShowNumberOfStars;
        }
    }
}