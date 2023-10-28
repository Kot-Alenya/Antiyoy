using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Progress.Data;
using CodeBase.Gameplay.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.Progress
{
    public static class WorldDataExtensions
    {
        public static SavedTileData ToSaved(this TileData tile)
        {
            var saved = new SavedTileData
            {
                Hex = tile.Hex.ToSaved(),
                RegionType = tile.Region.Type,
            };

            if (tile.Entity != null)
                saved.EntityType = tile.Entity.Type;

            return saved;
        }

        public static SavedHexPosition ToSaved(this HexPosition hex)
        {
            return new SavedHexPosition
            {
                Q = hex.Q,
                R = hex.R
            };
        }

        public static HexPosition FromSaved(this SavedHexPosition saved) => new(saved.Q, saved.R);

        public static SavedVector2Int ToSaved(this Vector2Int vector2Int)
        {
            return new SavedVector2Int
            {
                X = vector2Int.x,
                Y = vector2Int.y
            };
        }

        public static Vector2Int FromSaved(this SavedVector2Int saved) => new(saved.X, saved.Y);
    }
}