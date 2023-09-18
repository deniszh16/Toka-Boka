using Services.SceneLoader;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.UI
{
    [RequireComponent(typeof(Button))]
    public class SceneOpenButton : MonoBehaviour
    {
        [Header("Сцена для загрузки")]
        [SerializeField] private Scenes _scene;

        private Button _button;
        
        private ISceneLoaderService _sceneLoaderService;

        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService) =>
            _sceneLoaderService = sceneLoaderService;

        private void Awake() =>
            _button = GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(GoToScene);

        private void GoToScene() =>
            _sceneLoaderService.LoadSceneAsync(_scene, screensaver: true, delay: 0f);
        
        private void OnDisable() =>
            _button.onClick.RemoveListener(GoToScene);
    }
}