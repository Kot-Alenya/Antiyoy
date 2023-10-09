using CodeBase.Gameplay.World.Data.Hex;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Region.Model;
using CodeBase.Gameplay.World.Terrain.Data;
using CodeBase.Gameplay.World.Tile;
using CodeBase.Gameplay.World.Tile.Data;
using CodeBase.Gameplay.World.Tile.Model;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain
{
    public class TerrainFactory
    {
        private const string TerrainRootName = "Terrain";

        private readonly TerrainStaticData _terrainStaticData;
        private readonly TileFactory _tileFactory;
        private readonly RegionFactory _regionFactory;

        public TerrainFactory(StaticData staticData, TileFactory tileFactory, RegionFactory regionFactory)
        {
            _terrainStaticData = staticData.TerrainStaticData;
            _tileFactory = tileFactory;
            _regionFactory = regionFactory;
        }

        public TerrainModel Create()
        {
            var root = new GameObject(TerrainRootName).transform;
            var tiles = new TileArray(_terrainStaticData.Size);
            var tilesModel = new TilesModel(tiles, root, _tileFactory);
            var regionsModel = new RegionsModel(_regionFactory);
            var terrainModel = new TerrainModel(tilesModel, regionsModel);

            CreateBackground(root, _terrainStaticData.Size);

            return terrainModel;
        }

        private void CreateBackground(Transform root, Vector2Int size)
        {
            var instance = Object.Instantiate(_terrainStaticData.BackgroundPrefabData, root);

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