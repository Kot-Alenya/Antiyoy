using CodeBase.Gameplay.Terrain.Data.Hex;
using CodeBase.Gameplay.Terrain.Tile.Data;
using CodeBase.Infrastructure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Gameplay.Terrain.Tile
{
    public class TileFactory
    {
        private readonly TileStaticData _staticData;

        public TileFactory(StaticData data) => _staticData = data.TileStaticData;

        public TileData Create(Transform root, HexPosition hex)
        {
            var position = HexMath.ToWorldPosition(hex);
            var prefabData = CreatePrefabData(position, root);
            var tile = new TileData(prefabData, hex);

            return tile;
        }

        public void Destroy(TileData tile) => Object.Destroy(tile.GameObject);

        private TilePrefabData CreatePrefabData(Vector2 position, Transform root)
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