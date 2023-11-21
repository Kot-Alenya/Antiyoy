using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Tile;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainController : MonoBehaviour
    {
        private HexObjectCollection<TilePlace> _tilePlaces;

        public HexObjectCollection<TilePlace> TilePlaces => _tilePlaces;

        public void Initialize(HexObjectCollection<TilePlace> tilePlaces)
        {
            _tilePlaces = tilePlaces;
            ConnectTilePlaces();
        }


        private void ConnectTilePlaces()
        {
            foreach (var tile in _tilePlaces)
            foreach (var direction in HexCoordinatesDirections.Directions)
            {
                var neighbourTileIndex = tile.Hex + direction;

                if (!_tilePlaces.IsIndexValid(neighbourTileIndex))
                    continue;

                var neighbourTile = _tilePlaces.Get(neighbourTileIndex);

                tile.Connections.Add(neighbourTile);
            }
        }
    }
}