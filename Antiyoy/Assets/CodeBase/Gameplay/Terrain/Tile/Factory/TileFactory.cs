using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Terrain.Entity.Data;
using CodeBase.Gameplay.Terrain.Region;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Gameplay.Terrain.Tile.Collection;
using CodeBase.Gameplay.Terrain.Tile.Data;
using CodeBase.Infrastructure.Project.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Tile.Factory
{
    public class TileFactory : ITileFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly ITileCollection _tileCollection;
        private readonly RegionCollection _regionCollection;
        private Transform _tileRoot;

        public TileFactory(IStaticDataProvider staticDataProvider, ITileCollection tileCollection,
            RegionCollection regionCollection)
        {
            _staticDataProvider = staticDataProvider;
            _tileCollection = tileCollection;
            _regionCollection = regionCollection;
        }

        public void Initialize(Transform tileRoot) => _tileRoot = tileRoot;

        public void Create(HexPosition hex, RegionType regionType)
        {
            var instance = CreateInstance(hex, _tileRoot);
            var tile = new TileData(instance, hex);

            _tileCollection.Set(tile, hex);
            _regionCollection.AddTileToRegion(tile, regionType);
        }

        public void Destroy(TileData tile)
        {
            _tileCollection.Remove(tile.Hex);
            _regionCollection.RemoveTileFromRegion(tile);

            Object.Destroy(tile.Instance.gameObject);
        }

        public void Destroy(HexPosition hex) => Destroy(_tileCollection.Get(hex));

        public bool TryCreate(HexPosition hex, RegionType regionType)
        {
            if (!_tileCollection.TryGet(hex, out _))
            {
                Create(hex, regionType);
                return true;
            }

            return false;
        }

        public bool TryDestroy(HexPosition hex)
        {
            if (_tileCollection.TryGet(hex, out var tile))
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