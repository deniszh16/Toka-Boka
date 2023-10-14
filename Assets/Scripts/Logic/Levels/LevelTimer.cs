using System;
using UnityEngine;
using UnityEngine.UI;

namespace Logic.Levels
{
    [RequireComponent(typeof(Image))]
    public class LevelTimer : MonoBehaviour
    {
        public event Action TimerCompleted;
        
        private bool _activity;
        private float _seconds;
        private float _currentTime;
        private float _fillingImage;

        private Image _image;

        private void Awake() =>
            _image = GetComponent<Image>();

        private void Update()
        {
            if (_activity == false)
                return;
            
            _currentTime -= Time.deltaTime;
            _fillingImage = _currentTime / _seconds;
            _image.fillAmount = _fillingImage;

            if (_currentTime <= 0)
                TimerCompleted?.Invoke();
        }
        
        public int GetCurrentTime() =>
            (int)_currentTime;

        public void SetTimer()
        {
            _seconds = 30;
            _currentTime = _seconds;
        }

        public void ChangeTimerActivity(bool value) =>
            _activity = value;

        public void ChangeTimerSeconds(int value) =>
            _currentTime += value;
    }
}