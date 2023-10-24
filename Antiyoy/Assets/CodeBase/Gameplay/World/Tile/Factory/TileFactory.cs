using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Tile
{
    public class TileFactory : ITileFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly ITerrainTiles _terrainTiles;

        public TileFactory(IStaticDataProvider staticDataProvider, ITerrainTiles terrainTiles)
        {
            _staticDataProvider = staticDataProvider;
            _terrainTiles = terrainTiles;
        }

        public void Create(HexPosition hex, RegionType regionType)
        {
            var instance = CreateInstance(hex, _terrainTiles.TilesRoot);
            var tile = new TileData(instance, hex);

            _terrainTiles.Set(tile, hex, regionType);
        }

        public void Destroy(TileData tile)
        {
            _terrainTiles.Remove(tile);
            Object.Destroy(tile.Instance.gameObject);
        }

        public bool TryCreate(HexPosition hex, RegionType regionType)
        {
            if (!_terrainTiles.TryGet(hex, out _))
            {
                Create(hex, regionType);
                return true;
            }

            return false;
        }

        public bool TryDestroy(HexPosition hex)
        {
            if (_terrainTiles.TryGet(hex, out var tile))
            {
                Destroy(tile);
                return true;
            }

            return false;
        }

        private TilePrefabData CreateInstance(HexPosition hex, Transform root)
        {
            var position = HexMath.ToWorldPosition(hex);
            var tileStaticData = _staticDataProvider.Get<TileStaticData>();
            var instance = Object.Instantiate(tileStaticData.Prefab, root);
            var transform = instance.transform;

            transform.position = new Vector3(position.x, position.y, transform.position.z);

            return instance;
        }
    }
}