using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Gameplay.World.Tile
{
    public class TileFactory
    {
        private readonly IStaticDataProvider _staticDataProvider;

        public TileFactory(IStaticDataProvider staticDataProvider) => _staticDataProvider = staticDataProvider;

        public TileData Create(Transform root, HexPosition hex)
        {
            var position = HexMath.ToWorldPosition(hex);
            var prefabData = CreatePrefabData(position, root);
            var tile = new TileData(prefabData, hex);

            return tile;
        }

        public void DestroyInstance(TileData tile) => Object.Destroy(tile.Instance.gameObject);

        private TilePrefabData CreatePrefabData(Vector2 position, Transform root)
        {
            var tileStaticData = _staticDataProvider.Get<TileStaticData>();
            var prefab = tileStaticData.Prefab;
            var gameObjectPosition = new Vector3(position.x, position.y, 0);
            var rotation = Quaternion.identity;
            var gameObject = Object.Instantiate(prefab, gameObjectPosition, rotation);

            gameObject.transform.parent = root;

            return gameObject;
        }
    }
}