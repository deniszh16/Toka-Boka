using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using System;

namespace DZGames.TokaBoka.Services
{
    public class SceneLoaderService : MonoBehaviour, ISceneLoaderService
    {
        [Header("Экран затемнения")]
        [SerializeField] private CanvasGroup _blackout;

        public async UniTask LoadSceneAsync(int scene, bool screensaver, float delay, CancellationToken token)
        {
            if (delay > 0)
                await UniTask.Delay(TimeSpan.FromSeconds(delay), cancellationToken: token);
            
            if (screensaver)
            {
                _blackout.blocksRaycasts = true;
                _blackout.alpha = 1f;
            }
            
            await SceneManager.LoadSceneAsync(scene).WithCancellation(token);
            
            if (screensaver)
            {
                _blackout.blocksRaycasts = false;
                _blackout.alpha = 0;
            }
        }

        public async UniTask LoadLevelAsync(int levelNumber, CancellationToken token)
        {
            int level = (int)Scenes.Level + levelNumber - 1;
            await LoadSceneAsync(scene: level, screensaver: true, delay: 0f, token);
        }
    }
}