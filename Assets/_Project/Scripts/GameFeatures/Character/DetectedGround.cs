using System;
using UnityEngine;

namespace _Project.GameFeatures.Character
{
    [RequireComponent(typeof(BoxCollider))]
    public class DetectedGround : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private string _ground = "Ground";

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(_ground))
            {
                _player.ChangeIsGrounded(true);
                
                if (_player.CurrentYVelocity < 0)
                    _player.ChangeCurrentYVelocity(0);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(_ground))
                _player.ChangeIsGrounded(false);
        }
    }
}