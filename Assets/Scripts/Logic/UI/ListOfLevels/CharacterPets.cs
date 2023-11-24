using Services.PersistentProgress;
using Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.UI.ListOfLevels
{
    public class CharacterPets : MonoBehaviour
    {
        [Header("Изображения питомцев")]
        [SerializeField] private Image[] _pets;
        
        [Header("Кнопки покупки")]
        [SerializeField] private Button[] _buttons;
        
        [Header("Эффекты звезд")]
        [SerializeField] private ParticleSystem[] _effects;

        private int _characterNumber;

        private ParticleSystem _confetti;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        public void Construct(IPersistentProgressService progressService,
            ISaveLoadService saveLoadService, ParticleSystem confetti)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _confetti = confetti;
        }

        public void CheckPets(int characterNumber)
        {
            _characterNumber = characterNumber;
            bool[] characterPets = _progressService.UserProgress.CharacterPets[_characterNumber].Pets;

            for (int i = 0; i < characterPets.Length; i++)
            {
                if (characterPets[i])
                {
                    _pets[i].color = Color.white;
                    _buttons[i].gameObject.SetActive(false);
                }
            }
            
            ShowConfettiEffect();
        }

        private void ShowConfettiEffect()
        {
            if (_progressService.UserProgress.CharacterPets[_characterNumber].OpenPets >= 3)
            {
                _confetti.gameObject.SetActive(true);
                _confetti.Play();
            }
            else
            {
                _confetti.gameObject.SetActive(false);
            }
        }

        public void GetPet(int petNumber)
        {
            if (_progressService.UserProgress.Stars[_characterNumber] > 0)
            {
                _progressService.UserProgress.ChangeStars(levelNumber: _characterNumber, value: -1);
                _progressService.UserProgress.CharacterPets[_characterNumber].Pets[petNumber - 1] = true;
                _progressService.UserProgress.CharacterPets[_characterNumber].OpenPets++;
                _saveLoadService.SaveProgress();
                CheckPets(_characterNumber);
                ShowOpeningEffect(number: petNumber - 1);
            }
        }

        private void ShowOpeningEffect(int number)
        {
            _effects[number].gameObject.SetActive(true);
            _effects[number].Play();
        }
    }
}