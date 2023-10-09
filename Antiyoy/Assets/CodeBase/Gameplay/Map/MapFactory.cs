using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Map.Data;
using CodeBase.Gameplay.Region;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Region.Model;
using CodeBase.Gameplay.Tile;
using CodeBase.Gameplay.Tile.Data;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Gameplay.Map
{
    public class MapFactory
    {
        private readonly TerrainStaticData _staticData;
        private readonly TileFactory _tileFactory;
        private readonly RegionFactory _regionFactory;

        public MapFactory(StaticData data, TileFactory tileFactory, RegionFactory regionFactory)
        {
            _staticData = data.TerrainStaticData;
            _tileFactory = tileFactory;
            _regionFactory = regionFactory;
        }

        public MapController Create()
        {
            var root = new GameObject(nameof(MapController));
            var tiles = new TileArray(_staticData.Size);
            var regions = new RegionsModel(_regionFactory);
            var model = new TilesModel(tiles, regions, root, _tileFactory);
            var terrain = new MapController(model, regions);

            CreateBackground(root.transform, _staticData.Size);
            CreateTiles(terrain, _staticData.Size);
            terrain.RecalculateChangedRegions();

            return terrain;
        }

        private void CreateTiles(MapController terrain, Vector2Int size)
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