﻿using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region.Model;
using CodeBase.Gameplay.World.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Entity
{
    public class EntityFactory : IEntityFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;
        private readonly RegionsModel _regionsModel;
        private readonly TileCollection _tileCollection;

        public EntityFactory(IStaticDataProvider staticDataProvider, RegionsModel regionsModel,
            TileCollection tileCollection)
        {
            _staticDataProvider = staticDataProvider;
            _regionsModel = regionsModel;
            _tileCollection = tileCollection;
        }

        public void Create(TileData rootTile, EntityType entityType)
        {
            var presets = _staticDataProvider.Get<EntitiesPresetsCollection>();
            var preset = presets.Entities[entityType];
            var instance = Object.Instantiate(preset.Prefab, rootTile.Instance.transform);
            var entity = new EntityData(instance, entityType, preset.Income);

            rootTile.Entity = entity;
            _regionsModel.AddToChangedRegions(rootTile.Region);
        }

        public void Destroy(TileData rootTile)
        {
            var entity = rootTile.Entity;

            rootTile.Entity = null;
            _regionsModel.AddToChangedRegions(rootTile.Region);

            Object.Destroy(entity.Instance.gameObject);
        }

        public bool TryCreate(HexPosition hex, EntityType entityType)
        {
            if (_tileCollection.TryGet(hex, out var tile))
            {
                if (tile.Entity == null)
                    Create(tile, entityType);

                return true;
            }

            return false;
        }

        public bool TryDestroy(HexPosition hex)
        {
            if (_tileCollection.TryGet(hex, out var tile))
            {
                if (tile.Entity != null)
                    Destroy(tile);

                return true;
            }

            return true;
        }
    }
}