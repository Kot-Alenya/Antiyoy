using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Factory;
using CodeBase.Infrastructure.Project.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain
{
    public class TerrainFactory : ITerrainFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly ITileFactory _tileFactory;

        public TerrainFactory(IStaticDataProvider staticDataProvider, ITileFactory tileFactory)
        {
            _staticDataProvider = staticDataProvider;
            _tileFactory = tileFactory;
        }

        public void Create()
        {
            var staticData = _staticDataProvider.Get<TerrainStaticData>();
            var instance = CreateInstance(staticData);

            _tileFactory.Initialize(instance.transform);
        }

        private TerrainPrefabData CreateInstance(TerrainStaticData staticData)
        {
            var instance = Object.Instantiate(staticData.Prefab);
            StretchBackground(instance.BackgroundTransform, staticData);

            return instance;
        }

        private void StretchBackground(Transform backgroundTransform, TerrainStaticData staticData)
        {
            var maxArrayIndex = staticData.Size - Vector2Int.one;
            var halfTileSize = new Vector2(HexMath.InnerRadius, HexMath.OuterRadius);
            var lastPointOffset = maxArrayIndex.y % 2f == 0
                ? Vector2.right * HexMath.InnerRadius
                : Vector2.zero;

            var firstTileHex = HexMath.FromArrayIndex(Vector2Int.zero);
            var lastTileHex = HexMath.FromArrayIndex(maxArrayIndex);

            var firstPoint = HexMath.ToWorldPosition(firstTileHex) - halfTileSize;
            var lastPoint = HexMath.ToWorldPosition(lastTileHex) + halfTileSize + lastPointOffset;

            var scale = lastPoint - firstPoint;

            var center = firstPoint + scale / 2f;
            var position = new Vector3(center.x, center.y, backgroundTransform.position.z);

            backgroundTransform.localScale = scale;
            backgroundTransform.position = position;
        }
    }
}