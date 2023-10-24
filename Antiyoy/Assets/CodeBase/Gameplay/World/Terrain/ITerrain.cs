using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain
{
    public interface ITerrain
    {
        public bool IsHexInTerrain(HexPosition hex);
    }
}