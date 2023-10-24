using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Entity.Data
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