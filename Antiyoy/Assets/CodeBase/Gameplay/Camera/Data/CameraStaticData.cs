using UnityEngine;

namespace CodeBase.Gameplay.Camera.Data
{
    [CreateAssetMenu(menuName = "Configurations/Camera", fileName = "CameraConfig", order = 0)]
    public class CameraStaticData : ScriptableObject
    {
        public CameraObjectStaticData ObjectPrefab;
        public float MoveVelocity;
        public float ZoomVelocity;
        public float MinZoomValue;
        public float MaxZoomValue;
    }
}