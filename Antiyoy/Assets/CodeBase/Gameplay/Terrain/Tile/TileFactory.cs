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
            SetBiggestRadius(staticData.Prefab.Size);
            SetSmallerRadius();
        }

        public TileObject Create(Vector2 position, Transform root)
        {
            var tile = new TileObject();

            CreateObjectData(position, root);
            tile.Position = position;

            return tile;
        }

        public Vector2 GetTilePosition(int sideIndex, Vector2 parentPosition)
        {
            var offsetX = _tileBiggestRadius * 3 / 2;

            var position = sideIndex switch
            {
                TileSide.Down => parentPosition + new Vector2(0, -_tileSmallerRadius * 2),
                TileSide.Up => parentPosition + new Vector2(0, _tileSmallerRadius * 2),
                TileSide.UpRight => parentPosition + new Vector2(offsetX, _tileSmallerRadius),
                TileSide.DownRight => parentPosition + new Vector2(offsetX, -_tileSmallerRadius),
                TileSide.UpLeft => parentPosition + new Vector2(-offsetX, _tileSmallerRadius),
                TileSide.DownLeft => parentPosition + new Vector2(-offsetX, -_tileSmallerRadius),
                _ => default
            };

            var roundedPosition = new Vector2(
                (float)System.Math.Round(position.x, 4),
                (float)System.Math.Round(position.y, 4));
            
            return roundedPosition;
        }

        private void SetBiggestRadius(int tileSize) => _tileBiggestRadius = tileSize / 2f;

        private void SetSmallerRadius() => _tileSmallerRadius = Mathf.Sqrt(3) * _tileBiggestRadius / 2;

        private void CreateObjectData(Vector2 position, Transform root)
        {
            var prefab = _staticData.Prefab;
            var gameObjectPosition = new Vector3(position.x, position.y, 0);
            var rotation = Quaternion.identity;
            var gameObject = UnityEngine.Object.Instantiate(prefab, gameObjectPosition, rotation);

            gameObject.transform.parent = root;
        }
    }
}