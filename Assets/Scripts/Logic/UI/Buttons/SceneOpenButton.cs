using System.Threading;
using Cysharp.Threading.Tasks;
using TokaBoka.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TokaBoka.UI
{
    public class SceneOpenButton : MonoBehaviour
    {
        [Header("Компонент кнопки")]
        [SerializeField] private Button _button;
        
        [Header("Сцена для загрузки")]
        [SerializeField] private Scenes _scene;

        private ISceneLoaderService _sceneLoaderService;
        private CancellationTokenSource _cancellationTokenSource;
        
        [Inject]
        private void Construct(ISceneLoaderService sceneLoaderService) =>
            _sceneLoaderService = sceneLoaderService;

        private void OnEnable()
        {
            _button.onClick.AddListener(GoToScene);
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void GoToScene() =>
            _sceneLoaderService.LoadSceneAsync((int)_scene, screensaver: true, delay: 0f, _cancellationTokenSource.Token).Forget();
        
        private void OnDisable()
        {
            _button.onClick.RemoveListener(GoToScene);
            _cancellationTokenSource.Dispose();
        }
    }
}