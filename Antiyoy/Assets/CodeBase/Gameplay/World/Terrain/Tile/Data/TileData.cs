using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;

namespace CodeBase.Gameplay.World.Terrain.Tile.Data
{
    public class TileData
    {
        public readonly List<TileData> Neighbors = new();
        public readonly TilePrefabData Instance;
        public readonly HexPosition Hex;

        public RegionData Region;
        public UnitData Unit;

        public TileData(TilePrefabData instance, HexPosition hex)
        {
            Instance = instance;
            Hex = hex;
        }
    }
}