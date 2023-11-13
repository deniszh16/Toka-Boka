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
        
        [Header("Питомцы")]
        [SerializeField] private AssetReferenceGameObject[] _pets;

        [Header("Контейнер для персонажей")]
        [SerializeField] private Transform _container;
        
        [Header("Контейнер для питомцев")]
        [SerializeField] private Transform _petsContainer;

        private int _characterNumber;
        private int _selectedLevel;
        private GameObject _currentСharacter;
        private GameObject _currentPets;

        private IPersistentProgressService _progressService;
        private ISceneLoaderService _sceneLoaderService;
        private OpenPets _openPets;

        [Inject]
        private void Construct(IPersistentProgressService progressService,
            ISceneLoaderService sceneLoaderService, OpenPets openPets)
        {
            _progressService = progressService;
            _sceneLoaderService = sceneLoaderService;
            _openPets = openPets;
        }

        private void Start()
        {
            _selectedLevel = _progressService.UserProgress.Progress;
            _characterNumber = _selectedLevel - 1;
            _openPets.UpdateNumberOfPets(_characterNumber);
            
            CreateCharacter();
            CreatePet();
        }

        private void CreateCharacter() =>
            _characters[_characterNumber].InstantiateAsync(_container).Completed += OnCharacterInstantiated;

        private void OnCharacterInstantiated(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
                _currentСharacter = handle.Result;
        }

        private void CreatePet() =>
            _pets[_characterNumber].InstantiateAsync(_petsContainer).Completed += OnPetInstantiated;

        private void OnPetInstantiated(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
                _currentPets = handle.Result;
        }

        public void SelectLevel(int buttonNumber)
        {
            if (_characterNumber != buttonNumber - 1)
            {
                if (_currentСharacter)
                    _characters[_characterNumber].ReleaseInstance(_currentСharacter);
                
                if (_currentPets)
                    _pets[_characterNumber].ReleaseInstance(_currentPets);

                _selectedLevel = buttonNumber;
                _characterNumber = _selectedLevel - 1;
                _openPets.UpdateNumberOfPets(_characterNumber);
                
                CreateCharacter();
                CreatePet();
            }
        }

        public void GoToLevel() =>
            _sceneLoaderService.LoadLevelAsync(_selectedLevel);
    }
}