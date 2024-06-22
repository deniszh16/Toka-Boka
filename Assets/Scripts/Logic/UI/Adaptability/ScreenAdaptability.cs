using TokaBoka.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace TokaBoka.UI
{
    public class ScreenAdaptability : MonoBehaviour
    {
        [Header("Коэффециент переключения")]
        [SerializeField] private float _coefficient;
        
        [Header("Значение Match")]
        [SerializeField] private float _match;
        
        [Header("Ссылки на компоненты")]
        [SerializeField] private CanvasScaler _сanvasScaler;
        
        private int _lastScreenWidth;
        private int _lastScreenHeight;
        private float _aspectRatio;
        
        private IMonoUpdateService _monoUpdateService;

       [Inject]
        private void Construct(IMonoUpdateService monoUpdateService) =>
            _monoUpdateService = monoUpdateService;

        private void OnEnable()
        {
            _lastScreenWidth = Screen.width;
            _lastScreenHeight = Screen.height;
            _monoUpdateService.AddToUpdate(MyUpdate);
            
            ChangeMatchWidthOrHeight();
        }

        private void MyUpdate()
        {
            if (Screen.width == _lastScreenWidth && Screen.height == _lastScreenHeight)
                return;

            ChangeMatchWidthOrHeight();
        }

        private void ChangeMatchWidthOrHeight()
        {
            _aspectRatio = (float)Screen.width / Screen.height;
            _lastScreenWidth = Screen.width;
            _lastScreenHeight = Screen.height;
            
            _сanvasScaler.matchWidthOrHeight = _aspectRatio > _coefficient ? 1 : _match;
        }

        private void OnDisable() =>
            _monoUpdateService?.RemoveFromUpdate(MyUpdate);
    }
}