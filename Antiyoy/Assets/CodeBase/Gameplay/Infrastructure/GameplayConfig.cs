using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.PlayerCamera;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Tile;
using UnityEngine;

namespace CodeBase.Gameplay.Infrastructure
{
    [CreateAssetMenu(menuName = "Configurations/Gameplay/Gameplay", fileName = "GameplayConfig", order = 0)]
    public class GameplayConfig : ScriptableObject
    {
        public CameraConfig CameraConfig;
        public GameplayEcsConfig EcsConfig;
        public TerrainConfig TerrainConfig;
        public TileConfig TileConfig;
    }
}