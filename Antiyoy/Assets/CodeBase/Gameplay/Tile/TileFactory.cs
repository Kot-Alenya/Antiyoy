using CodeBase.Gameplay.CommonEcs;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Infrastructure;
using UnityEngine;

namespace CodeBase.Gameplay.Tile
{
    public class TileFactory
    {
        private readonly GameplayStaticDataProvider _staticDataProvider;
        private readonly GameplayEcsWorld _world;
        private Transform _tilePlaceRoot;
        private TileConfig _config;

        public TileFactory(GameplayStaticDataProvider staticDataProvider, GameplayEcsWorld world)
        {
            _staticDataProvider = staticDataProvider;
            _world = world;
        }

        public void Initialize(Transform tilePlaceRoot)
        {
            _tilePlaceRoot = tilePlaceRoot;
            _config = _staticDataProvider.GetTileConfig();
        }

        public TilePlace CreatePlace(HexCoordinates hex)
        {
            var tilePlace = Object.Instantiate(_config.PlacePrefab, hex.ToWorldPosition(), Quaternion.identity);

            tilePlace.transform.parent = _tilePlaceRoot;
            tilePlace.Initialize(hex, _world.NewEntity());
            
            return tilePlace;
        }
    }
}