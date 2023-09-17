using System;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Region.Data;
using CodeBase.Gameplay.Tile.Data;
using CodeBase.Infrastructure;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CodeBase.Gameplay.Tile
{
    public class TileFactory
    {
        private readonly TileStaticData _staticData;

        public TileFactory(StaticData data) => _staticData = data.TileStaticData;

        public TileObject Create(Transform root, HexPosition hex, RegionType regionType)
        {
            var position = HexMath.ToWorldPosition(hex);
            var prefabData = CreatePrefabData(position, root);
            var tile = new TileObject(prefabData, hex, regionType);

            prefabData.SpriteRenderer.color = GetColor(regionType);
            return tile;
        }

        public void Destroy(TileObject tile) => Object.Destroy(tile.GameObject);

        private TilePrefabData CreatePrefabData(Vector2 position, Transform root)
        {
            var prefab = _staticData.Prefab;
            var gameObjectPosition = new Vector3(position.x, position.y, 0);
            var rotation = Quaternion.identity;
            var gameObject = Object.Instantiate(prefab, gameObjectPosition, rotation);

            gameObject.transform.parent = root;

            return gameObject;
        }

        private Color GetColor(RegionType regionType)
        {
            return regionType switch
            {
                RegionType.Neutral => _staticData.NeutralColor,
                RegionType.Red => _staticData.RedColor,
                RegionType.Blue => _staticData.BlueColor,
                _ => throw new ArgumentOutOfRangeException(nameof(regionType), regionType, null)
            };
        }
    }
}