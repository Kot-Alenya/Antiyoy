using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Tile
{
    public class TileFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private Transform _tileRoot;

        public TileFactory(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public void Initialize(Transform tileRoot) => _tileRoot = tileRoot;

        public TileData Create(HexPosition hex)
        {
            var instance = CreateInstance(hex, _tileRoot);
            var tile = new TileData(instance, hex);

            return tile;
        }

        public void Destroy(TileData tile) => Object.Destroy(tile.Instance.gameObject);

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