using CodeBase.Infrastructure.Project.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.Camera.Data
{
    [CreateAssetMenu(menuName = "Configurations/Camera", fileName = "CameraConfig", order = 0)]
    public class CameraStaticData : ScriptableObject, IStaticData
    {
        public CameraPrefabData Prefab;
        public float MoveVelocity;
        public float ZoomVelocity;
        public float MinZoomValue;
        public float MaxZoomValue;
    }
}