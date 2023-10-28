using CodeBase.Infrastructure.Project.Services.StaticData.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Camera.Data
{
    [CreateAssetMenu(menuName = "Configurations/Camera", fileName = "CameraConfig", order = 0)]
    public class CameraStaticData : ScriptableObjectStaticData
    {
        public CameraPrefabData Prefab;
        public float MoveVelocity;
        public float ZoomVelocity;
        public float MinZoomValue;
        public float MaxZoomValue;
    }
}