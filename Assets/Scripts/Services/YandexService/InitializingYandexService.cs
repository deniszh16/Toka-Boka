using UnityEngine;
using Zenject;

namespace Services.YandexService
{
    public class InitializingYandexService : MonoBehaviour
    {
        private IYandexService _yandexService;

        [Inject]
        private void Construct(IYandexService yandexService) =>
            _yandexService = yandexService;

        private void Start()
        {
            if (_yandexService.Initialization == false)
                _yandexService.Init();
        }
    }
}