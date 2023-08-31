using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Tile.Object
{
    public class TileObject
    {
        public Dictionary<int, TileObject> Neighbors;
        public TileObjectData TileObjectData;
        public Vector2Int Index;
    }
}