using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Entity;
using CodeBase.Gameplay.World.Terrain.Region.Rebuilder;
using CodeBase.Gameplay.World.Terrain.Tile;
using CodeBase.Infrastructure.Services.ProgressSaveLoader.Watcher;

namespace CodeBase.Gameplay.World.Progress
{
    public class WorldProgressLoader : IProgressReader<WorldProgressData>
    {
        private readonly ITerrain _terrain;
        private readonly TileFactory _tileFactory;
        private readonly EntityFactory _entityFactory;
        private readonly RegionRebuilder _regionRebuilder;

        public WorldProgressLoader(ITerrain terrain, TileFactory tileFactory,
            EntityFactory entityFactory, RegionRebuilder regionRebuilder)
        {
            _terrain = terrain;
            _tileFactory = tileFactory;
            _entityFactory = entityFactory;
            _regionRebuilder = regionRebuilder;
        }

        public void OnProgressLoad(WorldProgressData progress)
        {
            /*
            _terrain.Initialize(progress.TerrainSize.FromSaved());

            foreach (var savedTile in progress.SavedTiles)
            {
                var hex = savedTile.Hex.FromSaved();

                _tileFactory.Create(hex, savedTile.RegionType);

                if (savedTile.EntityType != EntityType.None)
                    _entityFactory.Create(hex, savedTile.EntityType);
            }

            _regionRebuilder.RebuildFromBufferAndClearBuffer();
            */
        }
    }
}