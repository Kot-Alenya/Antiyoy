using UnityEngine;

namespace CodeBase.Gameplay.PlayerCamera
{
    [CreateAssetMenu(menuName = "Configurations/Gameplay/Camera", fileName = "CameraConfig", order = 0)]
    public class CameraConfig : ScriptableObject
    {
        public CameraController Prefab;
        public float MoveVelocity;
        public float ZoomVelocity;
    }
}