using System;

namespace TokaBoka.Services
{
    public interface IYandexService
    {
        public event Action<int> AdsViewed;

        public void Init();
        public void ShowFullScreenAds();
        public void ShowRewardedAds(int id);
    }
}