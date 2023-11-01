
using CodeBase.Gameplay.World.Tile;

namespace CodeBase.Gameplay.World.Terrain.Entity.Data
{
    public class EntityObject
    {
        public readonly EntityPrefabData Instance;
        public readonly TileObject RootTile;
        public readonly EntityType Type;
        public readonly int Income;

        public EntityObject(EntityPrefabData instance, TileObject rootTile, EntityType type, int income)
        {
            Instance = instance;
            RootTile = rootTile;
            Type = type;
            Income = income;
        }
    }
}