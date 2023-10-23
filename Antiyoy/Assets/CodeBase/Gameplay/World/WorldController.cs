using CodeBase.Gameplay.World.Terrain;

namespace CodeBase.Gameplay.World
{
    public class WorldController : IWorldController
    {
        public WorldController(IWorldTerrainController terrain) => Terrain = terrain;

        public IWorldTerrainController Terrain { get; }
    }
}