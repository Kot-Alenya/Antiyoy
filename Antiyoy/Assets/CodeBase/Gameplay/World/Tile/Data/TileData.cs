using System.Collections.Generic;
using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region.Data;

namespace CodeBase.Gameplay.World.Tile.Data
{
    public class TileData
    {
        public readonly List<TileData> Neighbors = new();
        public readonly TilePrefabData Instance;
        public readonly HexPosition Hex;

        public RegionData Region;
        public EntityData Entity;

        public TileData(TilePrefabData instance, HexPosition hex)
        {
            Instance = instance;
            Hex = hex;
        }
    }
}