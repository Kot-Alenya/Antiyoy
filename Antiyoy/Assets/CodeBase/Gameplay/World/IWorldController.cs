using CodeBase.Gameplay.World.Change;
using CodeBase.Gameplay.World.Change.Handler;
using CodeBase.Gameplay.World.Change.Recorder;
using CodeBase.Gameplay.World.Terrain;

namespace CodeBase.Gameplay.World
{
    public interface IWorldController
    {
        public IWorldTerrainController Terrain { get; }

        public IWorldChangeHandler ChangeHandler { get; }

        public IWorldChangeRecorder ChangeRecorder { get; }
    }
}