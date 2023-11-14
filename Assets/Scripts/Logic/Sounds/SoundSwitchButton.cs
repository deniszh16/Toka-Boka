using Services.Sound;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.Sounds
{
    [RequireComponent(typeof(Button))]
    public class SoundSwitchButton : MonoBehaviour
    {
        [Header("Компоненты кнопки")]
        [SerializeField] private Image _image;
        
        [Header("Иконки состояния")]
        [SerializeField] private Sprite _active;
        [SerializeField] private Sprite _inactive;
        
        private Button _button;
        private ISoundService _soundService;

        [Inject]
        private void Construct(ISoundService soundService) =>
            _soundService = soundService;

        private void Awake() =>
            _button = GetComponent<Button>();

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