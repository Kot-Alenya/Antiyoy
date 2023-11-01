using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using TMPro;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Tile.Data
{
    public class TilePrefabData : SerializedMonoBehaviour
    {
        public TextMeshProUGUI DebugText;
        public SpriteRenderer SpriteRenderer;
        public SpriteRenderer HideMaskSpriteRenderer;
        [OdinSerialize] public Dictionary<HexDirectionType, SpriteRenderer> Borders;
    }
}