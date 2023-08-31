using CodeBase.Gameplay.Terrain.Tile.Data;
using CodeBase.Gameplay.Terrain.Tile.Object;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Tile
{
    public class TileFactory
    {
        private TileStaticData _staticData;
        private TilePositionOffsets _positionOffsets;

        public void Initialize(TileStaticData staticData)
        {
            _staticData = staticData;
            _positionOffsets = CreatePositionOffsets(_staticData.Prefab.Size);
        }

        public TileObject Create(Transform root,Vector2 position, Vector2Int index)
        {
            var tile = new TileObject
            {
                TileObjectData = CreateObjectData(position, root),
                Index = index
            };

            return tile;
        }

        public TilePositionOffsets GetTilePositionOffsets() =>
            _positionOffsets;

        private TilePositionOffsets CreatePositionOffsets(int tileSize)
        {
            var tileBiggestRadius = tileSize / 2f;
            var tileSmallerRadius = Mathf.Sqrt(3) * tileBiggestRadius / 2;
            var tileSmallerDiameter = tileSmallerRadius * 2;
            var offsetX = tileBiggestRadius * 3 / 2;
            
            return new TilePositionOffsets
            {
                Up = new Vector2(0, tileSmallerDiameter),
                Down = new Vector2(0, -tileSmallerDiameter),

                RightUp = new Vector2(offsetX, tileSmallerRadius),
                RightDown = new Vector2(offsetX, -tileSmallerRadius),

                LeftUp = new Vector2(-offsetX, tileSmallerRadius),
                LeftDown = new Vector2(-offsetX, -tileSmallerRadius)
            };
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