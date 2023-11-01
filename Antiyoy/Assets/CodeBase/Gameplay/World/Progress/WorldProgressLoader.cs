﻿using CodeBase.Gameplay.World.Progress.SavedData;
using CodeBase.Gameplay.World.Terrain.Entity;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Region.Rebuild;
using CodeBase.Gameplay.World.Terrain.Tile.Collection;
using CodeBase.Gameplay.World.Terrain.Tile.Factory;
using CodeBase.Infrastructure.Services.ProgressSaveLoader.Watcher;

namespace CodeBase.Gameplay.World.Progress
{
    public class WorldProgressLoader : IProgressReader<WorldProgressData>
    {
        private readonly ITileCollection _tileCollection;
        private readonly ITileFactory _tileFactory;
        private readonly IEntityFactory _entityFactory;
        private readonly IRegionRebuilder _regionRebuilder;

        public WorldProgressLoader(ITileCollection tileCollection, ITileFactory tileFactory,
            IEntityFactory entityFactory, IRegionRebuilder regionRebuilder)
        {
            _tileCollection = tileCollection;
            _tileFactory = tileFactory;
            _entityFactory = entityFactory;
            _regionRebuilder = regionRebuilder;
        }

        public void OnProgressLoad(WorldProgressData progress)
        {
            _tileCollection.Initialize(progress.TerrainSize.FromSaved());

            foreach (var savedTile in progress.SavedTiles)
            {
                var hex = savedTile.Hex.FromSaved();

                _tileFactory.Create(hex, savedTile.RegionType);

                if (savedTile.EntityType != EntityType.None)
                    _entityFactory.Create(hex, savedTile.EntityType);
            }

            _regionRebuilder.RebuildFromBufferAndClearBuffer();
        }
    }
}