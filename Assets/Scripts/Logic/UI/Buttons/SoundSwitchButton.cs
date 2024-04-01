using Services.Sound;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI.Buttons
{
    public class SoundSwitchButton : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Button _button;
        [SerializeField] private Image _image;
        
        [Header("Иконки состояния")]
        [SerializeField] private Sprite _active;
        [SerializeField] private Sprite _inactive;

        private ISoundService _soundService;

        [Inject]
        private void Construct(ISoundService soundService) =>
            _soundService = soundService;

        private void Start() =>
            ChangeButtonIcon();

        private void OnEnable()
        {
            _button.onClick.AddListener(_soundService.SwitchSound);
            _soundService.SoundChanged += ChangeButtonIcon;
        }
        
        private void ChangeButtonIcon()
        {
            bool soundActivity = _soundService.SoundActivity;
            _image.sprite = soundActivity ? _active : _inactive;
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_soundService.SwitchSound);
            _soundService.SoundChanged -= ChangeButtonIcon;
        }
    }
}