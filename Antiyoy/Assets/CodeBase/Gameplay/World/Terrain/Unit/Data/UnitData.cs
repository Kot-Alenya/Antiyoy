using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.World.Terrain.Unit.Data
{
    public struct UnitData
    {
        public UnitPrefabData Instance;
        public UnitPresetData Preset;
        public TileData RootTile;
        public UnitType Type;
        public int Income;
        public int Cost;
        public int CostIncreaseFactor;
        public bool IsCanMove;
    }
}