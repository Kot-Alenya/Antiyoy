using System;
using CodeBase.Gameplay.Tile;

namespace _dev
{
    public class EntityFactory
    {
        private readonly EntityStaticData _staticData;

        public EntityFactory(EntityStaticData staticData) => _staticData = staticData;

        public IEntityController Create(EntityType entityType, TileObject rootTile)
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

        private CapitalController CreateCapital(TileObject rootTile)
        {
            var instance = UnityEngine.Object.Instantiate(_staticData.CapitalPrefab, rootTile.transform);
            var capital = new CapitalController();

            return capital;
        }
    }
}