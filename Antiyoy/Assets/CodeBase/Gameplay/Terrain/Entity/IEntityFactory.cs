using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Terrain.Entity.Data;
using CodeBase.Gameplay.Terrain.Tile.Data;

namespace CodeBase.Gameplay.Terrain.Entity
{
    public interface IEntityFactory
    {
        public void Create(HexPosition hex, EntityType entityType);

        public void Destroy(HexPosition hex);

        public bool TryCreate(HexPosition hex, EntityType entityType);

        public bool TryDestroy(HexPosition hex);
    }
}