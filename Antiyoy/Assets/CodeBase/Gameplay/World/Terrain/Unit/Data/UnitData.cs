using CodeBase.Gameplay.World.Terrain.Tile.Data;

namespace CodeBase.Gameplay.World.Terrain.Unit.Data
{
    public class UnitData
    {
        public readonly UnitType Type;
        public readonly UnitPrefabData Instance;
        public readonly TileData RootTile;
        public bool IsCanMove;

        public UnitData(UnitType type, UnitPrefabData instance, TileData rootTile, bool isCanMove)
        {
            Type = type;
            Instance = instance;
            RootTile = rootTile;
            IsCanMove = isCanMove;
        }
    }
}