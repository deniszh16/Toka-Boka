using TokaBoka.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TokaBoka.ListOfLevels
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
        [SerializeField] private RectTransform _buttonGlow;
        
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
            if (_number == _progressService.GetUserProgress.Progress)
            {
                CustomizeButton(_textNumber, _levelOpen);
                ShowButtonGlowEffect();
                
                ShowNumberOfStars();
                _progressService.GetUserProgress.StarsChanged += ShowNumberOfStars;
            }
            else if (_number < _progressService.GetUserProgress.Progress)
            {
                CustomizeButton(_iconComplete, _levelPassed);
                
                ShowNumberOfStars();
                _progressService.GetUserProgress.StarsChanged += ShowNumberOfStars;
            }
        }

        private void CustomizeButton(GameObject buttonElement, Sprite sprite)
        {
            _button.onClick.AddListener(SelectLevel);
            // TODO: Убрать, когда будут добавлены остальные уровни
            _button.interactable = _number < 2;

            buttonElement.SetActive(true);
            _iconLock.SetActive(false);
            _starsIcon.SetActive(true);
            _image.sprite = sprite;
        }

        private void SelectLevel()
        {
            _levelSelection.SelectLevel(_number);
            ShowButtonGlowEffect();
        }

        private void ShowNumberOfStars()
        {
            int stars = _progressService.GetUserProgress.GetNumberOfStars(_number - 1);
            _numberOfStars.ShowNumberOfStars(stars);
        }

        private void ShowButtonGlowEffect()
        {
            _buttonGlow.gameObject.SetActive(true);
            _buttonGlow.localPosition = gameObject.transform.localPosition;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(SelectLevel);
            _progressService.GetUserProgress.StarsChanged -= ShowNumberOfStars;
        }
    }
}