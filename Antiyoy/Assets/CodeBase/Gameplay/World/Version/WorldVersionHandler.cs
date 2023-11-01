using CodeBase.Gameplay.World.Terrain.Entity;
using CodeBase.Gameplay.World.Terrain.Region.Rebuild;
using CodeBase.Gameplay.World.Terrain.Tile.Factory;

namespace CodeBase.Gameplay.World.Version
{
    public class WorldVersionHandler
    {
        private readonly WorldVersionRecorder _worldVersionRecorder;
        private readonly ITileFactory _tileFactory;
        private readonly IEntityFactory _entityFactory;
        private readonly IRegionRebuilder _regionRebuilder;

        public WorldVersionHandler(WorldVersionRecorder worldVersionRecorder) => _worldVersionRecorder = worldVersionRecorder;

        public void ReturnBack()
        {
            if (!_worldVersionRecorder.TryGetPreviousRecord(out var record))
                return;

            for (var i = record.Length - 1; i >= 0; i--)
                record[i].Revert();
        }

        public void ReturnNext()
        {
            if (!_worldVersionRecorder.TryGetNextRecord(out var record))
                return;

            for (var i = 0; i < record.Length; i++)
                record[i].Apply();
        }
    }
}