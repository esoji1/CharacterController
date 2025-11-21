using UnityEngine;

namespace _Project.GameFeatures.Character
{
    public class Movement
    {
        private PlayerConfig _playerConfig;
        private Player _player;
        private CameraController _cameraController;

        private float _inputHorizontal;
        private float _inputVertical;
        private bool _isJump;

        public Movement(PlayerConfig playerConfig, Player player, CameraController cameraController)
        {
            _playerConfig = playerConfig;
            _player = player;
            _cameraController = cameraController;
        }

        public void Update() =>
            InputCharacter();

        public void FixedUpdate()
        {
            ApplyGravity();
            Jump();

            Vector3 moveDirection = GetCameraRelativeDirection(_inputHorizontal, _inputVertical);

            if (moveDirection != Vector3.zero)
            {
                Move(moveDirection);
                Rotate(moveDirection);
            }
        }

        private void InputCharacter()
        {
            _inputHorizontal = Input.GetAxis("Horizontal");
            _inputVertical = Input.GetAxis("Vertical");
            _isJump = Input.GetKey(KeyCode.Space) && _player.IsGrounded;
        }

        private void ApplyGravity()
        {
            if (_player.IsGrounded && _player.CurrentYVelocity < 0)
            {
                _player.ChangeCurrentYVelocity(-0.1f);
            }
            else
            {
                float newYVelocity = _player.CurrentYVelocity + _playerConfig.GravityForce * Time.fixedDeltaTime;
                _player.ChangeCurrentYVelocity(newYVelocity);
            }

            Vector3 velocity = _player.Rigidbody.linearVelocity;
            velocity.y = _player.CurrentYVelocity;
            _player.Rigidbody.linearVelocity = velocity;
        }

        private Vector3 GetCameraRelativeDirection(float horizontal, float vertical)
        {
            Vector3 cameraForward = _cameraController.transform.forward;
            Vector3 cameraRight = _cameraController.transform.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            return cameraForward * vertical + cameraRight * horizontal;
        }

        private void Move(Vector3 moveDirection)
        {
            Vector3 newVelocity = moveDirection * _playerConfig.MoveSpeed;
            newVelocity.y = _player.Rigidbody.linearVelocity.y;
            _player.Rigidbody.linearVelocity = newVelocity;
        }

        private void Rotate(Vector3 moveDirection)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            _player.transform.rotation = Quaternion.Slerp(_player.transform.rotation,
                targetRotation,
                _playerConfig.RotateSpeed * Time.fixedDeltaTime);
        }

        private void Jump()
        {
            if (_isJump == false)
                return;

            _player.ChangeCurrentYVelocity(_playerConfig.JumpForce);
            _player.ChangeIsGrounded(false);
        }
    }
}