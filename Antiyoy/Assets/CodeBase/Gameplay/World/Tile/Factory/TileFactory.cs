﻿using CodeBase.Gameplay.World.Entity;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region.Collection;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Terrain.Data;
using CodeBase.Gameplay.World.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Tile.Factory
{
    public class TileFactory : ITileFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly ITileCollection _tileCollection;
        private readonly RegionCollection _regionCollection;
        private readonly IEntityFactory _entityFactory;

        public TileFactory(IStaticDataProvider staticDataProvider, ITileCollection tileCollection,
            RegionCollection regionCollection, IEntityFactory entityFactory)
        {
            _staticDataProvider = staticDataProvider;
            _tileCollection = tileCollection;
            _regionCollection = regionCollection;
            _entityFactory = entityFactory;
        }

        public void Create(HexPosition hex, RegionType regionType)
        {
            var terrainStaticData = _staticDataProvider.Get<TerrainStaticData>();
            var instance = CreateInstance(hex, terrainStaticData.Instance.transform);
            var tile = new TileData(instance, hex);

            _tileCollection.Set(tile, hex);
            _regionCollection.AddToRegion(tile, regionType);
        }

        public void Destroy(TileData tile)
        {
            _entityFactory.TryDestroy(tile.Hex);
            _tileCollection.Remove(tile.Hex);
            _regionCollection.RemoveFromRegion(tile);

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