using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Tile
{
    public class TileFactory
    {
        private TileStaticData _staticData;
        private float _tileBiggestRadius;
        private float _tileSmallerRadius;

        public void Initialize(TileStaticData staticData)
        {
            _staticData = staticData;
            _tileBiggestRadius = staticData.Prefab.Size / 2f;
            _tileSmallerRadius = Mathf.Sqrt(3) * _tileBiggestRadius / 2;
        }

        public TileObject Create(Transform root, HexCoordinates coordinates)
        {
            var position = GetTilePosition(coordinates);
            var gameObject = CreateObjectData(position, root);
            var tile = new TileObject(gameObject, coordinates);

            return tile;
        }

        private Vector2 GetTilePosition(HexCoordinates coordinates)
        {
            var x = coordinates.X * _tileSmallerRadius * 2;
            var y = coordinates.Y * _tileBiggestRadius * 3 / 2;

            return coordinates.Y % 2 == 0 ? new Vector2(x - _tileSmallerRadius, y) : new Vector2(x, y);
        }

        private TileObjectStaticData CreateObjectData(Vector2 position, Transform root)
        {
            var prefab = _staticData.Prefab;
            var gameObjectPosition = new Vector3(position.x, position.y, 0);
            var rotation = Quaternion.identity;
            var gameObject = Object.Instantiate(prefab, gameObjectPosition, rotation);

            gameObject.transform.parent = root;

            return gameObject;
        }
    }
}