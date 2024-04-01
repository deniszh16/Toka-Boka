using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Logic.UI.Buttons
{
    public class OtherGamesButton : MonoBehaviour
    {
        [Header("Компонент кнопки")]
        [SerializeField] private Button _button;

        private void OnEnable() =>
            _button.onClick.AddListener(OpenLink);

        private void OpenLink() =>
            Application.OpenURL("https://yandex." + YandexGame.EnvironmentData.domain + "/games/developer/56836");

        private void OnDisable() =>
            _button.onClick.RemoveListener(OpenLink);
    }
}