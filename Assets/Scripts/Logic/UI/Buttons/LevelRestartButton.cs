using Services.SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI.Buttons
{
    [RequireComponent(typeof(Button))]
    public class LevelRestartButton : MonoBehaviour
    {
        private const string Prefix = "Level_";
        
        private Button _button;
        private ISceneLoaderService _sceneLoader;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService) =>
            _sceneLoader = sceneLoaderService;

        private void Awake() =>
            _button = GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(ReloadLevel);

        private void ReloadLevel()
        {
            string currentScene = _sceneLoader.CurrentScene;
            string levelNumber = currentScene.Substring(Prefix.Length);
            int number = int.Parse(levelNumber);
            _sceneLoader.LoadLevelAsync(number);
        }

        private void OnDisable() =>
            _button.onClick.RemoveListener(ReloadLevel);
    }
}