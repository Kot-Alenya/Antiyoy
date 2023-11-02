using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Tile
{
    public class TileObject
    {
        public readonly List<TileObject> Neighbors = new();
        public readonly TilePrefabData Instance;
        public readonly HexPosition Hex;

        public EntityObject Entity;

        public TileObject(TilePrefabData instance, HexPosition hex)
        {
            Instance = instance;
            Hex = hex;
        }

        public RegionObject Region { get; private set; }


        public void SetRegion(RegionObject region)
        {
            Instance.SpriteRenderer.color = region.Color;
            Region = region;
        }
    }
}