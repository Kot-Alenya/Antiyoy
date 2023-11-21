using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Infrastructure;
using CodeBase.Gameplay.Tile;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainFactory
    {
        private readonly GameplayStaticDataProvider _staticDataProvider;
        private readonly TileFactory _tileFactory;
        private readonly TerrainControllerProvider _controllerProvider;
        private TerrainConfig _config;

        public TerrainFactory(GameplayStaticDataProvider staticDataProvider, TileFactory tileFactory,
            TerrainControllerProvider controllerProvider)
        {
            _staticDataProvider = staticDataProvider;
            _tileFactory = tileFactory;
            _controllerProvider = controllerProvider;
        }

        public void Initialize() => _config = _staticDataProvider.GetTerrainConfig();

        public void Create()
        {
            var controller = Object.Instantiate(_config.Controller);
            var collection = new HexObjectCollection<TilePlace>(_config.Size);

            _tileFactory.Initialize(controller.transform);

            for (var y = 0; y < _config.Size.y; y++)
            for (var x = 0; x < _config.Size.x; x++)
            {
                var hex = HexCoordinatesUtilities.FromArrayIndex(new Vector2Int(x, y));
                collection.Set(_tileFactory.CreatePlace(hex), hex);
            }

            controller.Initialize(collection);
            _controllerProvider.Initialize(controller);
        }
    }
}