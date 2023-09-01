using CodeBase.Gameplay.Terrain.Tile.Data;
using CodeBase.Gameplay.Terrain.Tile.Object;
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

        public TileObject Create(Transform root, Vector2Int offsetCoordinates)
        {
            var position = GetTilePosition(offsetCoordinates);
            var tile = new TileObject
            {
                TileObjectData = CreateObjectData(position, root),
                Coordinates = HexCoordinates.FromOffset(offsetCoordinates)
            };

            return tile;
        }

        private Vector2 GetTilePosition(Vector2Int index)
        {
            var x = index.x * _tileSmallerRadius * 2;
            var y = index.y * _tileBiggestRadius * 3 / 2;

            return index.y % 2 == 0 ? new Vector2(x - _tileSmallerRadius, y) : new Vector2(x, y);
        }

        private TileObjectData CreateObjectData(Vector2 position, Transform root)
        {
            var prefab = _staticData.Prefab;
            var gameObjectPosition = new Vector3(position.x, position.y, 0);
            var rotation = Quaternion.identity;
            var gameObject = UnityEngine.Object.Instantiate(prefab, gameObjectPosition, rotation);

            gameObject.transform.parent = root;

            return gameObject;
        }
    }
}