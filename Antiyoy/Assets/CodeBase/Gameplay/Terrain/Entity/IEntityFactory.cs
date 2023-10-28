using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Terrain.Entity.Data;

namespace CodeBase.Gameplay.Terrain.Entity
{
    public interface IEntityFactory
    {
        void Create(HexPosition hex, EntityType entityType);
        void Destroy(HexPosition hex);
        bool TryCreate(HexPosition hex, EntityType entityType);
        bool TryDestroy(HexPosition hex);
    }
}