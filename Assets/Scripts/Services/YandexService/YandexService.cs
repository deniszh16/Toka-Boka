using System;
using UnityEngine;
using YG;

namespace TokaBoka.Services
{
    public class YandexService : MonoBehaviour, IYandexService
    {
        public event Action<int> AdsViewed;

        public void Init() =>
            YandexGame.RewardVideoEvent += GiveRewardForViewing;

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