using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.World.Terrain.Entity.Data
{
    public class EntityData
    {
        public readonly EntityPrefabData Instance;
        public readonly TileData RootTile;
        public readonly EntityType Type;
        public readonly EntityPresetData Preset;
        public bool IsCanMove;

        public EntityData(EntityPrefabData instance, TileData rootTile, EntityType type, EntityPresetData preset, bool isCanMove)
        {
            Instance = instance;
            RootTile = rootTile;
            Type = type;
            Preset = preset;
            IsCanMove = isCanMove;
        }
    }
}