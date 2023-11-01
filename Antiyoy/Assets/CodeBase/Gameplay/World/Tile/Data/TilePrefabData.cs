using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Gameplay.World.Tile.Data
{
    public class TilePrefabData : SerializedMonoBehaviour
    {
        [OdinSerialize] public Dictionary<HexDirectionType, SpriteRenderer> Borders;
        public SpriteRenderer SpriteRenderer;
    }
}