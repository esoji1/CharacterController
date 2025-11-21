using UnityEngine;

namespace _Project.GameFeatures.Character
{
    public class DetectedGround : MonoBehaviour
    {
        private const string Ground = "Ground";

        [SerializeField] private Player _player;

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(Ground))
            {
                _player.ChangeIsGrounded(true);
                if (_player.CurrentYVelocity < 0)
                    _player.ChangeCurrentYVelocity(0);
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(Ground))
                _player.ChangeIsGrounded(false);
        }
    }
}