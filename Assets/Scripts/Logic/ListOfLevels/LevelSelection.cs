using System.Threading;
using TokaBoka.Services;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using VContainer;

namespace TokaBoka.ListOfLevels
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
        
        [Header("Эффекты сцены")]
        [SerializeField] private ParticleSystem _confetti;
        [SerializeField] private ParticleSystem _receivingEffect;

        [Header("Иконка загрузки")]
        [SerializeField] private GameObject _loadingAnObject;

        private bool _creation;
        private int _characterNumber;
        private int _selectedLevel;
        private GameObject _currentСharacter;
        private GameObject _currentPets;

        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;
        private ISceneLoaderService _sceneLoaderService;
        private ISoundService _soundService;
        
        private OpenPets _openPets;
        
        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            ISceneLoaderService sceneLoaderService, ISoundService soundService, OpenPets openPets)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _sceneLoaderService = sceneLoaderService;
            _soundService = soundService;
            _openPets = openPets;
        }

        private void Start()
        {
            _selectedLevel = _progressService.GetUserProgress.Progress;
            
            // TODO: Убрать, когда будут добавлены остальные уровни
            if (_selectedLevel >= 1) _selectedLevel = 1;
            
            _characterNumber = _selectedLevel - 1;
            _openPets.UpdateNumberOfPets(_characterNumber);
            
            CreateCharacter();
            CreatePet();
        }

        private void CreateCharacter()
        {
            _creation = true;
            _loadingAnObject.SetActive(true);
            _characters[_characterNumber].InstantiateAsync(_container).Completed += OnCharacterInstantiated;
        }

        private void OnCharacterInstantiated(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _loadingAnObject.SetActive(false);
                _currentСharacter = handle.Result;
                _creation = false;
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
                    characterPets.Construct(_progressService, _saveLoadService, _confetti, _receivingEffect);
                    characterPets.CheckPets(_characterNumber);
                }
            }
        }

        public void SelectLevel(int buttonNumber)
        {
            if (_creation == false && _characterNumber != buttonNumber - 1)
            {
                CharacterCleanup();
                
                _selectedLevel = buttonNumber;
                _characterNumber = _selectedLevel - 1;
                _openPets.UpdateNumberOfPets(_characterNumber);
                
                CreateCharacter();
                CreatePet();
            }
        }

        private void CharacterCleanup()
        {
            if (_currentСharacter)
                _characters[_characterNumber].ReleaseInstance(_currentСharacter);
                
            if (_currentPets)
                _pets[_characterNumber].ReleaseInstance(_currentPets);
        }

        public void GoToLevel()
        {
            _soundService.StopBackgroundMusic();
            _sceneLoaderService.LoadLevelAsync(_selectedLevel, CancellationToken.None);
        }

        private void OnDestroy() =>
            CharacterCleanup();
    }
}