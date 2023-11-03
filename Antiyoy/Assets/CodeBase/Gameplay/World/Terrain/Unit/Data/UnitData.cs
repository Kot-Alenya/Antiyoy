using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.World.Terrain.Unit.Data
{
    public struct UnitData
    {
        public UnitPrefabData Instance;
        public UnitPresetData Preset;
        public TileData RootTile;
        public UnitType Type;
        public bool IsCanMove;
    }
}