using CodeBase.Gameplay.Terrain.Tile.Data;

namespace CodeBase.Gameplay.Terrain.Entity.Data
{
    public class EntityData
    {
        public readonly EntityPrefabData Instance;
        public readonly TileData RootTile;
        public readonly EntityType Type;
        public readonly int Income;

        public EntityData(EntityPrefabData instance, TileData rootTile, EntityType type, int income)
        {
            Instance = instance;
            RootTile = rootTile;
            Type = type;
            Income = income;
        }
    }
}