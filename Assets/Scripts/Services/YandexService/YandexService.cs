using System;
using UnityEngine;
using YG;

namespace Services.YandexService
{
    public class YandexService : MonoBehaviour, IYandexService
    {
        public event Action<int> AdsViewed;
        
        public bool Initialization { get; private set; }

        public void Init()
        {
            YandexGame.RewardVideoEvent += GiveRewardForViewing;
            Initialization = true;
        }

        public void ShowFullScreenAds() =>
            YandexGame.FullscreenShow();

        public void ShowRewardedAds(int id) =>
            YandexGame.RewVideoShow(id);
        
        private void GiveRewardForViewing(int id) =>
            AdsViewed?.Invoke(id);

        private void OnDestroy() =>
            YandexGame.RewardVideoEvent -= GiveRewardForViewing;
    }
}