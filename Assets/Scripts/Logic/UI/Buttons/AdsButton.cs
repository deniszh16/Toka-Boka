using DZGames.TokaBoka.Services;
using UnityEngine;
using VContainer;

namespace DZGames.TokaBoka.UI
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