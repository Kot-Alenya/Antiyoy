using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Tile.Collection
{
    public interface ITileCollection : IEnumerable<TileData>
    {
        Vector2Int Size { get; }
        int Count { get; }
        void Initialize(Vector2Int size);
        bool IsInCollection(HexPosition hex);
        void Set(TileData tile, HexPosition hex);
        void Remove(HexPosition hex);
        TileData Get(HexPosition hex);
        TileData Get(int index);
        bool TryGet(HexPosition hex, out TileData tile);
    }
}