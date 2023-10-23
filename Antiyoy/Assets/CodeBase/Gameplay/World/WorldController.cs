using CodeBase.Gameplay.World.Terrain;

namespace CodeBase.Gameplay.World
{
    public class WorldController : IWorldController
    {
        public WorldController(ITerrain terrain) => Terrain = terrain;

        public ITerrain Terrain { get; }
    }
}