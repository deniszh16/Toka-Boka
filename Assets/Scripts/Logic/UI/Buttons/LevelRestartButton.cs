using Logic.Levels;
using Services.SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class LevelRestartButton : MonoBehaviour
    {
        private Button _button;
        private CurrentLevel _currentLevel;
        private ISceneLoaderService _sceneLoader;

        [Inject]
        private void Construct(CurrentLevel currentLevel, ISceneLoaderService sceneLoaderService)
        {
            _currentLevel = currentLevel;
            _sceneLoader = sceneLoaderService;
        }

        private void Awake() =>
            _button = GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(ReloadLevel);

        private void ReloadLevel() =>
            _sceneLoader.LoadLevelAsync(_currentLevel.LevelNumber);

        private void OnDisable() =>
            _button.onClick.RemoveListener(ReloadLevel);
    }
}