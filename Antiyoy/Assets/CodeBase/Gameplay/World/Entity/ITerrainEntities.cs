using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Entity
{
    public interface ITerrainEntities
    {
        public void Set(EntityData entity, TileData rootTile);

        public void Remove(EntityData entity);
    }
}