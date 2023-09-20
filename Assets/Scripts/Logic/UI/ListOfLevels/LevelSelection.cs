using Services.PersistentProgress;
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
        private GameObject _currentСharacter;

        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(IPersistentProgressService progressService) =>
            _progressService = progressService;

        private void Start()
        {
            _characterNumber = _progressService.UserProgress.Progress - 1;
            CreateCharacter();
        }

        private void CreateCharacter() =>
            _characters[_characterNumber].InstantiateAsync(_container).Completed += OnCharacterInstantiated;

        private void OnCharacterInstantiated(AsyncOperationHandle<GameObject> handle)
        {
            if (handle.Status == AsyncOperationStatus.Succeeded)
                _currentСharacter = handle.Result;
        }

        public void SelectLevel(int characterNumber)
        {
            if (_characterNumber != characterNumber - 1)
            {
                if (_currentСharacter)
                    _characters[_characterNumber].ReleaseInstance(_currentСharacter);

                _characterNumber = characterNumber - 1;
                CreateCharacter();
            }
        }
    }
}