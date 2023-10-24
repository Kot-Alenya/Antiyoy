using CodeBase.Gameplay.World.Entity;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Tile
{
    public class TileFactory : ITileFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly Transform _tileRoot;
        private readonly TileCollection _tileCollection;
        private readonly EntityFactory _entityFactory;
        private readonly ITerrainRegions _terrainRegions;

        public TileFactory(IStaticDataProvider staticDataProvider, Transform tileRoot, TileCollection tileCollection,
            EntityFactory entityFactory, ITerrainRegions terrainRegions)
        {
            _staticDataProvider = staticDataProvider;
            _tileRoot = tileRoot;
            _tileCollection = tileCollection;
            _entityFactory = entityFactory;
            _terrainRegions = terrainRegions;
        }

        public void Create(HexPosition hex, RegionType regionType)
        {
            var instance = CreateInstance(hex, _tileRoot);
            var tile = new TileData(instance, hex);

            _tileCollection.Set(tile, hex);
            TileUtilities.ConnectWithNeighbors(tile, _tileCollection);
            _terrainRegions.AddToRegion(tile, regionType);
        }

        public void Destroy(TileData tile)
        {
            if (tile.Entity != null)
                _entityFactory.Destroy(tile);

            TileUtilities.DisconnectFromNeighbors(tile);
            _tileCollection.Remove(tile.Hex);
            _terrainRegions.RemoveFromRegion(tile);

            Object.Destroy(tile.Instance.gameObject);
        }

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