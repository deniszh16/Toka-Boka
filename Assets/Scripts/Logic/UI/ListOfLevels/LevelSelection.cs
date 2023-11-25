using Services.PersistentProgress;
using Services.SaveLoad;
using Services.SceneLoader;
using Services.Sound;
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
        
        [Header("Питомцы персонажей")]
        [SerializeField] private AssetReferenceGameObject[] _pets;

        [Header("Контейнеры для объектов")]
        [SerializeField] private Transform _container;
        [SerializeField] private Transform _petsContainer;
        
        [Header("Эффект конфетти")]
        [SerializeField] private ParticleSystem _confetti;

        [Header("Иконка загрузки")]
        [SerializeField] private GameObject _loadingAnObject;

        private int _characterNumber;
        private int _selectedLevel;
        private GameObject _currentСharacter;
        private GameObject _currentPets;

        private OpenPets _openPets;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private ISceneLoaderService _sceneLoaderService;
        private ISoundService _soundService;

        [Inject]
        private void Construct(OpenPets openPets, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService, ISceneLoaderService sceneLoaderService, ISoundService soundService)
        {
            _openPets = openPets;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _sceneLoaderService = sceneLoaderService;
            _soundService = soundService;
        }

        private void Start()
        {
            _selectedLevel = _progressService.UserProgress.Progress;
            if (_selectedLevel >= 7) _selectedLevel = 6;
            _characterNumber = _selectedLevel - 1;
            _openPets.UpdateNumberOfPets(_characterNumber);
            
            CreateCharacter();
            CreatePet();
        }

        private void CreateCharacter()
        {
            _loadingAnObject.SetActive(true);
            _characters[_characterNumber].InstantiateAsync(_container).Completed += OnCharacterInstantiated;
        }

        private void OnCharacterInstantiated(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _loadingAnObject.SetActive(false);
                _currentСharacter = handle.Result;
            }
        }

        private void CreatePet() =>
            _pets[_characterNumber].InstantiateAsync(_petsContainer).Completed += OnPetInstantiated;

        private void OnPetInstantiated(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _currentPets = handle.Result;
                if (_currentPets.TryGetComponent(out CharacterPets characterPets))
                {
                    characterPets.Construct(_progressService, _saveLoadService, _confetti);
                    characterPets.CheckPets(_characterNumber);
                }
            }
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

        public void GoToLevel()
        {
            _soundService.StopBackgroundMusic();
            _sceneLoaderService.LoadLevelAsync(_selectedLevel);
        }
    }
}