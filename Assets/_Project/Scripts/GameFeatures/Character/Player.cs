using UnityEngine;

namespace _Project.GameFeatures.Character
{
    [RequireComponent(typeof(DetectedGround), typeof(CapsuleCollider),
        typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private CameraController _cameraController;
        
        private Movement _movementTwo;
        
        private Rigidbody _rigidbody;

        private bool _isGrounded;
        private float _currentYVelocity;

        public bool IsGrounded => _isGrounded;
        public float CurrentYVelocity => _currentYVelocity;
        public Rigidbody Rigidbody => _rigidbody;

        private void Awake() =>
            ExtractComponents();
        
        private void Start() =>
            Initialize();

        private void Update() =>
            _movementTwo.Update();

        private void FixedUpdate() =>
            _movementTwo.FixedUpdate();
        
        public void ChangeCurrentYVelocity(float velocity) =>
            _currentYVelocity = velocity;
        
        public void ChangeIsGrounded(bool isGrounded) =>
            _isGrounded = isGrounded;
        
        private void Initialize()
        {
            _cameraController.FollowPlayer(transform);
            _rigidbody.useGravity = false;
            _movementTwo = new Movement(_playerConfig, this, _cameraController);
        }

        private void ExtractComponents()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
    }
}