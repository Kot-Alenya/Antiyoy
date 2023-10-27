using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Entity.Data;

namespace CodeBase.Gameplay.World.Terrain.Entity
{
    public interface IEntityFactory
    {
        void Create(HexPosition hex, EntityType entityType);

        void Destroy(HexPosition hex);

        bool TryDestroy(HexPosition hex);
    }
}