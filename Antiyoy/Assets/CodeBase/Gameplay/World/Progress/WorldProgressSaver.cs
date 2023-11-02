using System.Collections.Generic;
using CodeBase.Gameplay.World.Progress.SavedData;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Infrastructure.Services.ProgressSaveLoader.Watcher;

namespace CodeBase.Gameplay.World.Progress
{
    public class WorldProgressSaver : IProgressWriter<WorldProgressData>
    {
        private readonly ITerrain _tileCollection;

        public WorldProgressSaver(ITerrain tileCollection) => _tileCollection = tileCollection;

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