using System.Collections.Generic;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Terrain.Entity.Data;
using CodeBase.Gameplay.Terrain.Region.Data;

namespace CodeBase.Gameplay.Terrain.Tile.Data
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