using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Tile.Object
{
    public class TileObjectData : MonoBehaviour
    {
        public List<TileObject> Neighbours = new();
        public TextMeshProUGUI DebugText;
        public int Size;
    }
}