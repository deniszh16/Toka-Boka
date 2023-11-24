using Logic.Levels;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI.Buttons
{
    public class TogglePause : MonoBehaviour
    {
        [Header("Компонент кнопки")]
        [SerializeField] private Button _button;
        
        [Header("Включение паузы")]
        [SerializeField] private bool _pauseSetting;
        
        private GamePause _gamePause;
        
        [Inject]
        private void Construct(GamePause gamePause) =>
            _gamePause = gamePause;

        private void OnEnable() =>
            _button.onClick.AddListener(() => _gamePause.TogglePause(_pauseSetting));

        private void OnDisable() =>
            _button.onClick.RemoveAllListeners();
    }
}