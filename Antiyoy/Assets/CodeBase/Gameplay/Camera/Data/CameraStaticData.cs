using CodeBase.Gameplay.World.Terrain;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.Camera.Data
{
    [CreateAssetMenu(menuName = "Configurations/Camera", fileName = "CameraConfig", order = 0)]
    public class CameraStaticData : ScriptableObject, IStaticData
    {
        public CameraObjectStaticData ObjectPrefab;
        public float MoveVelocity;
        public float ZoomVelocity;
        public float MinZoomValue;
        public float MaxZoomValue;
    }
}