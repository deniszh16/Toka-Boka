using System.Threading;
using TokaBoka.Levels;
using TokaBoka.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TokaBoka.UI
{
    public class LevelRestartButton : MonoBehaviour
    {
        [Header("Компонент кнопки")]
        [SerializeField] private Button _button;

        private ISceneLoaderService _sceneLoader;
        private CurrentLevel _currentLevel;
        
        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService, CurrentLevel currentLevel)
        {
            _sceneLoader = sceneLoaderService;
            _currentLevel = currentLevel;
        }

        private void OnEnable() =>
            _button.onClick.AddListener(ReloadLevel);

        private void ReloadLevel() =>
            _sceneLoader.LoadLevelAsync(_currentLevel.LevelNumber, CancellationToken.None);

        private void OnDisable() =>
            _button.onClick.RemoveListener(ReloadLevel);
    }
}