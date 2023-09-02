using CodeBase.Gameplay.Camera.Input;
using UnityEngine;

namespace CodeBase.Gameplay.Camera.Data
{
    [CreateAssetMenu(menuName = "Configurations/Camera", fileName = "CameraConfig", order = 0)]
    public class CameraStaticData : ScriptableObject
    {
        public CameraObjectStaticData ObjectPrefab;
        public CameraInput[] InputPrefabs;
        public float MoveVelocity;
        public float ZoomVelocity;
    }
}