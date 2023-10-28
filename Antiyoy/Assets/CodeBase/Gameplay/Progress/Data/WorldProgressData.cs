using System;
using CodeBase.Infrastructure.Project.Services.ProgressSaveLoader;

namespace CodeBase.Gameplay.Progress.Data
{
    [Serializable]
    public class WorldProgressData : ProgressData
    {
        public SavedTileData[] SavedTiles;
        public SavedVector2Int TerrainSize;
    }
}