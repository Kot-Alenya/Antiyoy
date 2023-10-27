using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Tile.Collection
{
    public interface ITileCollection : IEnumerable<TileData>
    {
        public Vector2Int Size { get; }

        public bool IsInCollection(HexPosition hex);

        public void Set(TileData tile, HexPosition hex);

        public void Remove(HexPosition hex);

        public TileData Get(HexPosition hex);

        public bool TryGet(HexPosition hex, out TileData tile);
    }
}