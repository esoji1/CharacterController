using UnityEngine;

namespace _Project.GameFeatures.Character
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/New PlayerConfig", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [field: SerializeField] public float MoveSpeed { get; private set; } = 5f;
        [field: SerializeField] public float RotateSpeed { get; private set; } = 10f;
        [field: SerializeField] public float JumpForce { get; private set; } = 20f;
        [field: SerializeField] public float GravityForce { get; private set; } = -30;
        [field: SerializeField] public float Sensitivity { get; private set; } = 5f;
        [field: SerializeField] public Vector2 CameraLimit { get; private set; } = new(-45, 40);
        [field: SerializeField] public Vector2 FOVLimit { get; private set; } = new(40, 90);
    }
}