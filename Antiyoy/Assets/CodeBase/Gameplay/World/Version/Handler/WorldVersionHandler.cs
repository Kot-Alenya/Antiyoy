using CodeBase.Gameplay.World.Terrain.Entity;
using CodeBase.Gameplay.World.Terrain.Region.Rebuild;
using CodeBase.Gameplay.World.Terrain.Tile.Factory;
using CodeBase.Gameplay.World.Version.Operation;
using CodeBase.Gameplay.World.Version.Recorder;

namespace CodeBase.Gameplay.World.Version.Handler
{
    public class WorldVersionHandler : IWorldVersionHandler
    {
        private readonly IWorldVersionRecorder _worldVersionRecorder;
        private readonly ITileFactory _tileFactory;
        private readonly IEntityFactory _entityFactory;
        private readonly IRegionRebuilder _regionRebuilder;

        public WorldVersionHandler(IWorldVersionRecorder worldVersionRecorder, ITileFactory tileFactory,
            IEntityFactory entityFactory,
            IRegionRebuilder regionRebuilder)
        {
            _worldVersionRecorder = worldVersionRecorder;
            _tileFactory = tileFactory;
            _entityFactory = entityFactory;
            _regionRebuilder = regionRebuilder;
        }

        public void ReturnBack()
        {
            if (!_worldVersionRecorder.TryGetPreviousRecord(out var data))
                return;

            for (var i = data.Length - 1; i >= 0; i--)
                Revert(data[i]);
        }

        public void ReturnNext()
        {
            if (!_worldVersionRecorder.TryGetNextRecord(out var data))
                return;

            for (var i = 0; i < data.Length; i++)
                Apply(data[i]);
        }

        private void Revert(IOperationData operation)
        {
            switch (operation)
            {
                case CreateTileOperationData data:
                    _tileFactory.Destroy(data.Hex);
                    break;
                case DestroyTileOperationData data:
                    _tileFactory.Create(data.Hex, data.RegionType);
                    break;
                case CreateEntityOperationData data:
                    _entityFactory.Destroy(data.Hex);
                    break;
                case DestroyEntityOperationData data:
                    _entityFactory.Create(data.Hex, data.EntityType);
                    break;
            }

            _regionRebuilder.RebuildFromBufferAndClearBuffer();
        }

        private void Apply(IOperationData operation)
        {
            switch (operation)
            {
                case CreateTileOperationData data:
                    _tileFactory.Create(data.Hex, data.RegionType);
                    break;
                case DestroyTileOperationData data:
                    _tileFactory.Destroy(data.Hex);
                    break;
                case CreateEntityOperationData data:
                    _entityFactory.Create(data.Hex, data.EntityType);
                    break;
                case DestroyEntityOperationData data:
                    _entityFactory.Destroy(data.Hex);
                    break;
            }

            _regionRebuilder.RebuildFromBufferAndClearBuffer();
        }
    }
}