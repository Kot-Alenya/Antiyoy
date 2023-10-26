using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Hex;

namespace CodeBase.Gameplay.World.Entity
{
    public interface IEntityFactory
    {
        public void Create(HexPosition hex, EntityType entityType);

        public void Destroy(HexPosition hex);

        public bool TryDestroy(HexPosition hex);
    }
}