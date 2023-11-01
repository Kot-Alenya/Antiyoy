using System;
using CodeBase.Gameplay.World.Progress.SavedData;
using CodeBase.Infrastructure.Services.ProgressSaveLoader;

namespace CodeBase.Gameplay.World.Progress
{
    [Serializable]
    public class WorldProgressData : ProgressData
    {
        public SavedTileData[] SavedTiles;
        public SavedVector2Int TerrainSize;
    }
}