using Services.SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI.Buttons
{
    public class SceneOpenButton : MonoBehaviour
    {
        [Header("Компонент кнопки")]
        [SerializeField] private Button _button;
        
        [Header("Сцена для загрузки")]
        [SerializeField] private Scenes _scene;

        private ISceneLoaderService _sceneLoaderService;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService) =>
            _sceneLoaderService = sceneLoaderService;

        private void OnEnable() =>
            _button.onClick.AddListener(GoToScene);

        private void GoToScene() =>
            _sceneLoaderService.LoadSceneAsync(_scene, screensaver: true, delay: 0f);
        
        private void OnDisable() =>
            _button.onClick.RemoveListener(GoToScene);
    }
}