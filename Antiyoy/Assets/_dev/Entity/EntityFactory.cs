using System;
using CodeBase.Gameplay.Terrain.Tile;
using CodeBase.Gameplay.Terrain.Tile.Data;

namespace _dev
{
    public class EntityFactory
    {
        private readonly EntityStaticData _staticData;

        public EntityFactory(EntityStaticData staticData) => _staticData = staticData;

        public IEntityController Create(EntityType entityType, TileData rootTile)
        {
            switch (entityType)
            {
                case EntityType.Capital:
                    return CreateCapital(rootTile);
                case EntityType.Pine:
                    break;
                case EntityType.Peasant:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(entityType), entityType, null);
            }

            return default;
        }

        private CapitalController CreateCapital(TileData rootTile)
        {
            //var instance = UnityEngine.Object.Instantiate(_staticData.CapitalPrefab, rootTile.transform);
            var capital = new CapitalController();

            return capital;
        }
    }
}