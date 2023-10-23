using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Entity
{
    public interface IEntityFactory
    {
        public void Create(TileData rootTile, EntityType entityType);

        public void Destroy(TileData rootTile);

        public bool TryCreate(HexPosition hex, EntityType entityType);

        public bool TryDestroy(HexPosition hex);
    }
}