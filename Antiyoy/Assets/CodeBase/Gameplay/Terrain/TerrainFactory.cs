using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Tile;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainFactory
    {
        private readonly TerrainStaticData _staticData;
        private readonly TileFactory _tileFactory;

        public TerrainFactory(StaticData data, TileFactory tileFactory)
        {
            _staticData = data.TerrainStaticData;
            _tileFactory = tileFactory;
        }

        public TerrainController Create()
        {
            var root = new GameObject(nameof(TerrainController));
            var tiles = new TerrainTiles(_staticData.Size);
            var regions = new TerrainRegions();
            var model = new TerrainModel(tiles, regions, root, _tileFactory, _staticData.Size);
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
            var instance = Object.Instantiate(_staticData.BackgroundPrefabData, root, true);
            var middleHexIndex = (size - Vector2Int.one) / 2;
            var middleHexPosition = HexMath.ToWorldPosition(HexMath.FromArrayIndex(middleHexIndex));
            var xPositionOffset = middleHexPosition.x + HexMath.InnerRadius / 2f;
            var position = new Vector3(xPositionOffset, middleHexPosition.y, instance.transform.position.z);

            instance.Transform.localScale = new Vector3(size.x - 1, size.y - 2);
            instance.Transform.position = position;
        }
    }
}