using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Factory;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Tile.Factory
{
    public class TileFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly Transform _tileRoot;

        public TileFactory(IStaticDataProvider staticDataProvider, Transform tileRoot)
        {
            _staticDataProvider = staticDataProvider;
            _tileRoot = tileRoot;
        }

        public TileObject Create(HexPosition hex)
        {
            var instance = CreateInstance(hex, _tileRoot);
            var tile = new TileObject(instance, hex);

            return tile;
        }

        public void Destroy(TileObject tile) => Object.Destroy(tile.Instance.gameObject);

        private TilePrefabData CreateInstance(HexPosition hex, Transform root)
        {
            var position = HexMath.ToWorldPosition(hex);
            var tileStaticData = _staticDataProvider.Get<TilesConfig>();
            var instance = Object.Instantiate(tileStaticData.Prefab, root);
            var transform = instance.transform;

            transform.position = new Vector3(position.x, position.y, transform.position.z);

            return instance;
        }
    }
}