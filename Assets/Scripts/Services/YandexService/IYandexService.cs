using System;

namespace Services.YandexService
{
    public interface IYandexService
    {
        public event Action<int> AdsViewed;

        public bool Initialization { get; }

        public void Init();
        public void ShowFullScreenAds();
        public void ShowRewardedAds(int id);
    }
}