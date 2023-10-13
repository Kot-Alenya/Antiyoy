using CodeBase.Gameplay.World.Change.Handler;
using CodeBase.Gameplay.World.Change.Recorder;
using CodeBase.Gameplay.World.Terrain;

namespace CodeBase.Gameplay.World
{
    public class WorldController : IWorldController
    {
        public WorldController(IWorldTerrainController terrain, IWorldChangeHandler changeHandler,
            IWorldChangeRecorder changeRecorder)
        {
            Terrain = terrain;
            ChangeHandler = changeHandler;
            ChangeRecorder = changeRecorder;
        }

        public IWorldTerrainController Terrain { get; }

        public IWorldChangeHandler ChangeHandler { get; }
        
        public IWorldChangeRecorder ChangeRecorder { get; }
    }
}