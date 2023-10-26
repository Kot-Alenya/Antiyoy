using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Hex;

namespace CodeBase.Gameplay.World.Entity
{
    public interface IEntityFactory
    {
        void Create(HexPosition hex, EntityType entityType);

        void Destroy(HexPosition hex);

        bool TryDestroy(HexPosition hex);
    }
}