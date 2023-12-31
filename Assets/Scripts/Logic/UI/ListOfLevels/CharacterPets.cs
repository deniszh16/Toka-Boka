﻿using Services.PersistentProgress;
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

        private int _characterNumber;

        private ParticleSystem _confetti;
        private ParticleSystem _receivingEffect;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        public void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            ParticleSystem confetti, ParticleSystem receivingEffect)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _confetti = confetti;
            _receivingEffect = receivingEffect;
        }

        public void CheckPets(int characterNumber)
        {
            _characterNumber = characterNumber;
            bool[] characterPets = _progressService.GetUserProgress.CharacterPets[_characterNumber].Pets;

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
            if (_progressService.GetUserProgress.CharacterPets[_characterNumber].OpenPets >= 3)
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
            if (_progressService.GetUserProgress.Stars[_characterNumber] > 0)
            {
                _progressService.GetUserProgress.ChangeStars(levelNumber: _characterNumber, value: -1);
                _progressService.GetUserProgress.CharacterPets[_characterNumber].Pets[petNumber - 1] = true;
                _progressService.GetUserProgress.CharacterPets[_characterNumber].OpenPets++;
                _saveLoadService.SaveProgress();
                CheckPets(_characterNumber);
                ShowOpeningEffect(number: petNumber - 1);
            }
        }

        private void ShowOpeningEffect(int number)
        {
            _receivingEffect.transform.position = _pets[number].transform.position;
            _receivingEffect.gameObject.SetActive(true);
            _receivingEffect.Play();
        }
    }
}