using Logic.Levels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class TogglePause : MonoBehaviour
    {
        [Header("Включение паузы")]
        [SerializeField] private bool _pauseSetting;
        
        private Button _button;
        private GamePause _gamePause;
        
        [Inject]
        private void Construct(GamePause gamePause) =>
            _gamePause = gamePause;

        private void Awake() =>
            _button = GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(() => _gamePause.TogglePause(_pauseSetting));

        private void OnDisable() =>
            _button.onClick.RemoveAllListeners();
    }
}