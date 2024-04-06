using Services.YandexService;
using UnityEngine;
using Zenject;

namespace Logic.UI.Buttons
{
    public class AdsButton : MonoBehaviour
    {
        private IYandexService _yandexService;

        [Inject]
        private void Construct(IYandexService yandexService) =>
            _yandexService = yandexService;

        public void ShowAds() =>
            _yandexService.ShowFullScreenAds();
    }
}