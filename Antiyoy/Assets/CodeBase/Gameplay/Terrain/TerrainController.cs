using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Tile;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain
{
    public class TerrainController : MonoBehaviour
    {
        private HexObjectCollection<TilePlace> _tilePlaceCollection;

        public void Initialize(HexObjectCollection<TilePlace> tilePlaceCollection)
        {
            _tilePlaceCollection = tilePlaceCollection;
            ConnectTilePlaces();
        }

        private void ConnectTilePlaces()
        {
            foreach (var tile in _tilePlaceCollection)
            foreach (var direction in HexCoordinatesDirections.Directions)
            {
                var neighbourTileIndex = tile.Hex + direction;

                if (!_tilePlaceCollection.IsIndexValid(neighbourTileIndex))
                    continue;

                var neighbourTile = _tilePlaceCollection.Get(neighbourTileIndex);

                tile.Connections.Add(neighbourTile);
            }
        }
    }
}