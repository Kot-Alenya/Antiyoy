using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Terrain.Data.Hex;
using CodeBase.Gameplay.Terrain.Region;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Gameplay.Terrain.Tile;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainFactory
    {
        private readonly TerrainStaticData _staticData;
        private readonly TileFactory _tileFactory;
        private readonly RegionFactory _regionFactory;

        public TerrainFactory(StaticData data, TileFactory tileFactory, RegionFactory regionFactory)
        {
            _staticData = data.TerrainStaticData;
            _tileFactory = tileFactory;
            _regionFactory = regionFactory;
        }

        public TerrainController Create()
        {
            var root = new GameObject(nameof(TerrainController));
            var tiles = new TerrainTiles(_staticData.Size);
            var regions = new TerrainRegions(_regionFactory);
            var model = new TerrainModel(tiles, regions, root, _tileFactory);
            var terrain = new TerrainController(model);

            CreateBackground(root.transform, _staticData.Size);
            CreateTiles(terrain, _staticData.Size);
            terrain.RecalculateChangedRegions();

            return terrain;
        }

        private void CreateTiles(TerrainController terrain, Vector2Int size)
        {
            for (var y = 0; y < size.y; y++)
            for (var x = 0; x < size.x; x++)
            {
                var arrayIndex = new Vector2Int(x, y);
                var hex = HexMath.FromArrayIndex(arrayIndex);
                terrain.CreateTile(hex, RegionType.Neutral);
            }
        }

        private void CreateBackground(Transform root, Vector2Int size)
        {
            var instance = Object.Instantiate(_staticData.BackgroundPrefabData, root);

            var maxArrayIndex = size - Vector2Int.one;
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
            var position = new Vector3(center.x, center.y, instance.Transform.position.z);

            instance.Transform.localScale = scale;
            instance.Transform.position = position;
        }
    }
}