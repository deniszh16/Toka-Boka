using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Logic.Levels
{
    public class CameraMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Границы экрана")]
        [SerializeField, Range(-100, 0)] private float _leftBorder;
        [SerializeField, Range(0, 100)] private float _rightBorder;

        [Header("Виртуальная камера")]
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        
        public bool Activity { get; set; }

        private Camera _camera;
        private Transform _virtualCameraTransform;
        private Vector3 _virtualCameraPosition;

        private bool _movingVirtualCamera;
        private Vector3 _touchPosition;
        private Vector3 _direction;
        
        private void Awake()
        {
            _camera = Camera.main;
            _virtualCameraTransform = _virtualCamera.transform;
        }

        private void Update()
        {
            if (Activity == false || _movingVirtualCamera == false)
                return;
            
            _direction = _touchPosition - _camera.ScreenToWorldPoint(Input.mousePosition);
            _direction.y = 0;
            _virtualCameraPosition += _direction;
            
            CheckOutOfBounds();
            _virtualCameraTransform.position = _virtualCameraPosition;
        }
        
        private void CheckOutOfBounds()
        {
            float clamp = Mathf.Clamp(_virtualCameraPosition.x, _leftBorder, _rightBorder);
            _virtualCameraPosition.x = clamp;
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _virtualCameraPosition = _virtualCameraTransform.position;
            _touchPosition = _camera.ScreenToWorldPoint(Input.mousePosition);
            _movingVirtualCamera = true;
        }
        
        public void OnPointerUp(PointerEventData eventData) =>
            _movingVirtualCamera = false;
    }
}