using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Tile.Data;
using CodeBase.Infrastructure;
using UnityEngine;

namespace CodeBase.Gameplay.Tile
{
    public class TileFactory
    {
        private readonly TileStaticData _staticData;

        public TileFactory(StaticData data) => _staticData = data.TileStaticData;

        public TileObject Create(Transform root, HexPosition hex)
        {
            var position = HexMath.ToWorldPosition(hex);
            var prefabData = CreatePrefabData(position, root);
            var tile = prefabData.gameObject.AddComponent<TileObject>();

            tile.Constructor(prefabData, hex);

            return tile;
        }

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