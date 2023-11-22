using UnityEngine;

namespace CodeBase.Gameplay.Tile
{
    public class TileController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public void SetColor(Color color) => _spriteRenderer.color = color;
    }
}