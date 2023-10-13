using CodeBase.Gameplay.World.Change.Recorder;
using CodeBase.Gameplay.World.Data.Operation;
using CodeBase.Gameplay.World.Terrain;

namespace CodeBase.Gameplay.World.Change.Handler
{
    public class WorldChangeHandler : IWorldChangeHandler
    {
        private readonly WorldChangeRecorder _recorder;
        private readonly IWorldTerrainController _terrainController;

        public WorldChangeHandler(WorldChangeRecorder recorder, IWorldTerrainController terrainController)
        {
            _recorder = recorder;
            _terrainController = terrainController;
        }

        public void ReturnWorldBack()
        {
            if (!_recorder.TryBack(out var data))
                return;

            for (var i = data.Length - 1; i >= 0; i--)
                Revert(data[i]);

            _terrainController.RecalculateChangedRegions();
        }

        public void ReturnWorldNext()
        {
            if (!_recorder.TryNext(out var data))
                return;

            for (var i = 0; i < data.Length; i++)
                Apply(data[i]);

            _terrainController.RecalculateChangedRegions();
        }

        private void Revert(IWorldOperationData operation)
        {
            switch (operation)
            {
                case WorldCreateTileOperationData data:
                    _terrainController.TryDestroyTile(data.Hex);
                    break;
                case WorldDestroyTileOperationData data:
                    _terrainController.TryCreateTile(data.Hex, data.RegionType);
                    break;
            }
        }

        private void Apply(IWorldOperationData operation)
        {
            switch (operation)
            {
                case WorldCreateTileOperationData data:
                    _terrainController.TryCreateTile(data.Hex, data.RegionType);
                    break;
                case WorldDestroyTileOperationData data:
                    _terrainController.TryDestroyTile(data.Hex);
                    break;
            }
        }
    }
}