using System;
using UnityEngine;

namespace _Project.GameFeatures.Character
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] public PlayerConfig _playerConfig;
        [SerializeField] private bool _clickToMoveCamera;
        [SerializeField] private bool _canZoom = true;

        private Transform _playerTransform;

        private float _mouseX;
        private float _mouseY;

        public Camera MainCamera { get; private set; }
    
        private void Awake() =>
            MainCamera =  Camera.main;

        private void Update()
        {
            if (_playerTransform == null)
                return;

            transform.position = _playerTransform.position + new Vector3(0, 2f, 0);

            if (_canZoom && Input.GetAxis("Mouse ScrollWheel") != 0)
            {
                float scrollInput = Input.GetAxis("Mouse ScrollWheel") *
                                    _playerConfig.Sensitivity * 2;
                float newFOV = MainCamera.fieldOfView - scrollInput;

                MainCamera.fieldOfView = Mathf.Clamp(newFOV, _playerConfig.FOVLimit.x,
                    _playerConfig.FOVLimit.y);
            }

            if (_clickToMoveCamera)
                if (Input.GetAxisRaw("Fire2") == 0)
                    return;

            _mouseX += Input.GetAxis("Mouse X") * _playerConfig.Sensitivity;
            _mouseY += Input.GetAxis("Mouse Y") * _playerConfig.Sensitivity;
            _mouseY = Mathf.Clamp(_mouseY, _playerConfig.CameraLimit.x, _playerConfig.CameraLimit.y);
            transform.rotation = Quaternion.Euler(-_mouseY, _mouseX, 0);
        }

        public void FollowPlayer(Transform playerTransform)
        {
            _playerTransform = playerTransform;

            if (_clickToMoveCamera == false)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }
}