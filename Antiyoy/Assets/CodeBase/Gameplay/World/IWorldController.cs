using CodeBase.Gameplay.World.Terrain;

namespace CodeBase.Gameplay.World
{
    public interface IWorldController
    {
        public ITerrain Terrain { get; }
    }
}