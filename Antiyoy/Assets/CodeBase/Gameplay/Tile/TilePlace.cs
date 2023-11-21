using System.Collections.Generic;
using CodeBase.Gameplay.Hex;
using UnityEngine;

namespace CodeBase.Gameplay.Tile
{
    public class TilePlace : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;

        public void Initialize(HexCoordinates hex, int entityId)
        {
            Hex = hex;
            EntityId = entityId;
            Disable();
        }

        public List<TilePlace> Connections;
        
        public HexCoordinates Hex { get; private set; }

        public int EntityId { get; private set; }

        public void Enable() => _spriteRenderer.enabled = true;

        public void Disable() => _spriteRenderer.enabled = false;
    }
}