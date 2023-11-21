using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Infrastructure;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainFactory
    {
        private readonly GameplayStaticDataProvider _staticDataProvider;
        private TerrainConfig _config;

        public TerrainFactory(GameplayStaticDataProvider staticDataProvider) =>
            _staticDataProvider = staticDataProvider;

        public void Initialize() => _config = _staticDataProvider.GetTerrainConfig();

        public void Create()
        {
            for (var y = 0; y < _config.Size.y; y++)
            for (var x = 0; x < _config.Size.x; x++)
            {
                var hex = HexCoordinatesUtilities.FromArrayIndex(new Vector2Int(x, y));
                CreateTile(hex);
            }
        }

        private void CreateTile(HexCoordinates hex) =>
            Object.Instantiate(_config.TilePrefab, hex.ToWorldPosition(), Quaternion.identity);
    }
}