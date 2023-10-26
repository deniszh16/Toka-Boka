using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class OpeningLink : MonoBehaviour
    {
        private Button _button;
        private const string YandexGames = "https://yandex.ru/games/developer?name=DZ%20Games";

        private void Awake() =>
            _button = GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(OpenDeveloperPage);

        private void OpenDeveloperPage() =>
            Application.OpenURL(YandexGames);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OpenDeveloperPage);
    }
}