using Services.PersistentProgress;
using Services.SceneLoader;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

namespace Logic.UI.ListOfLevels
{
    public class LevelSelection : MonoBehaviour
    {
        [Header("Персонажи")]
        [SerializeField] private AssetReferenceGameObject[] _characters;

        [Header("Контейнер для персонажей")]
        [SerializeField] private Transform _container;

        private int _characterNumber;
        private int _selectedLevel;
        private GameObject _currentСharacter;

        private IPersistentProgressService _progressService;
        private ISceneLoaderService _sceneLoaderService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISceneLoaderService sceneLoaderService)
        {
            _progressService = progressService;
            _sceneLoaderService = sceneLoaderService;
        }

        private void Start()
        {
            _selectedLevel = _progressService.UserProgress.Progress;
            _characterNumber = _selectedLevel - 1;
            CreateCharacter();
        }

        private void CreateCharacter() =>
            _characters[_characterNumber].InstantiateAsync(_container).Completed += OnCharacterInstantiated;

        private void OnCharacterInstantiated(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
                _currentСharacter = handle.Result;
        }

        public void SelectLevel(int buttonNumber)
        {
            if (_characterNumber != buttonNumber - 1)
            {
                if (_currentСharacter)
                    _characters[_characterNumber].ReleaseInstance(_currentСharacter);

                _selectedLevel = buttonNumber;
                _characterNumber = _selectedLevel - 1;
                CreateCharacter();
            }
        }

        public void GoToLevel() =>
            _sceneLoaderService.LoadLevelAsync(_selectedLevel);
    }
}