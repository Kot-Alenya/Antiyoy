using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Entity.Data;

namespace CodeBase.Gameplay.World.Terrain.Entity
{
    public interface IEntityFactory
    {
        public void Create(HexPosition hex, EntityType entityType);

        public void Destroy(HexPosition hex);

        public bool TryCreate(HexPosition hex, EntityType entityType);

        public bool TryDestroy(HexPosition hex);
    }
}