using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Tile
{
    public class TileObject
    {
        public readonly List<TileData> Neighbors = new();
        public readonly TilePrefabData Instance;
        public readonly HexPosition Hex;

        public RegionObject Region;
        public EntityData Entity;

        public TileObject(TilePrefabData instance, HexPosition hex)
        {
            Instance = instance;
            Hex = hex;
        }
    }
}