using System.Collections.Generic;
using CodeBase.Gameplay.Hex;
using UnityEngine;

namespace CodeBase.Gameplay.Tile
{
    public class TilePlace : MonoBehaviour
    {
        public List<TilePlace> Connections;
        public HexCoordinates Hex;
        public int EntityId;
    }
}