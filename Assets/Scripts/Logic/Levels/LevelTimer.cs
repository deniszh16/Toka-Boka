using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.Levels
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

        private Coroutine _timerCoroutine;

        public void SetTimer()
        {
            _seconds = 30;
            _currentTime = _seconds;
            UpdateTimerScale();
        }

        public int GetCurrentTime() =>
            (int)_currentTime;

        public void StartTimer() =>
            _timerCoroutine = StartCoroutine(StartTimerCoroutine());

        private IEnumerator StartTimerCoroutine()
        {
            WaitForSeconds second = new WaitForSeconds(0.5f);
            while (_currentTime > 0)
            {
                UpdateTimerScale();
                yield return second;
                _currentTime -= 0.5f;
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

        public void ChangeTimerSeconds(int value) =>
            _currentTime += value;
    }
}