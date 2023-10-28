using System.Collections.Generic;
using CodeBase.Gameplay.World.Progress.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Collection;
using CodeBase.Infrastructure.Project.Services.ProgressSaveLoader.Watcher;

namespace CodeBase.Gameplay.World.Progress
{
    public class WorldProgressSaver : IProgressWriter<WorldProgressData>
    {
        private readonly ITileCollection _tileCollection;

        public WorldProgressSaver(ITileCollection tileCollection) => _tileCollection = tileCollection;

        public void OnProgressSave(WorldProgressData progress)
        {
            var savedTiles = new List<SavedTileData>();

            foreach (var tile in _tileCollection)
            {
                if (tile != null)
                    savedTiles.Add(tile.ToSaved());
            }

            progress.SavedTiles = savedTiles.ToArray();
            progress.TerrainSize = _tileCollection.Size.ToSaved();
        }
    }
}