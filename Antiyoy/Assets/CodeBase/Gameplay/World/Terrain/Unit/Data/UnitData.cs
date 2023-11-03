using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.World.Terrain.Entity.Data
{
    public class UnitData
    {
        public readonly UnitPrefabData Instance;
        public readonly TileData RootTile;
        public readonly UnitType Type;
        public readonly UnitPresetData Preset;
        public bool IsCanMove;

        public UnitData(UnitPrefabData instance, TileData rootTile, UnitType type, UnitPresetData preset, bool isCanMove)
        {
            Instance = instance;
            RootTile = rootTile;
            Type = type;
            Preset = preset;
            IsCanMove = isCanMove;
        }
    }
}