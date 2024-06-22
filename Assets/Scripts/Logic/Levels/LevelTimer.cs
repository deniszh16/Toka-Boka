using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DZGames.TokaBoka.Levels
{
    public class LevelTimer : MonoBehaviour
    {
        [Header("Компонент изображения")]
        [SerializeField] private Image _image;
        
        public event Action TimerCompleted;
        
        private bool _activity;
        private float _seconds;
        private float _currentTime;
        private float _fillingImage;
        private int _difficultyLevel;

        private Coroutine _timerCoroutine;

        public void SetDifficultyLevel(int attempts)
        {
            if (attempts > 2) attempts = 2;
            _difficultyLevel = 15 * attempts;
        }

        public void SetTimer()
        {
            _seconds = 60 - _difficultyLevel;
            _currentTime = _seconds;
            UpdateTimerScale();
        }
        
        public void ChangeTimerSeconds(int value) =>
            _currentTime += value;

        public void ChangeTimerActivity(bool value) =>
            _activity = value;

        public void StartTimer() =>
            _timerCoroutine = StartCoroutine(StartTimerCoroutine());

        private IEnumerator StartTimerCoroutine()
        {
            WaitForSeconds second = new WaitForSeconds(1f);
            while (_currentTime > 0)
            {
                yield return second;
                
                if (_activity)
                {
                    _currentTime -= 1f;
                    UpdateTimerScale();
                }
            }
            
            TimerCompleted?.Invoke();
        }

        private void UpdateTimerScale()
        {
            _fillingImage = _currentTime / _seconds;
            _image.fillAmount = _fillingImage;
        }

        public void StopTimer()
        {
            if (_timerCoroutine != null)
                StopCoroutine(_timerCoroutine);
        }
    }
}