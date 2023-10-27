using System;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Infrastructure.Services.ProgressSaveLoader;

namespace CodeBase.Gameplay.World
{
    [Serializable]
    public class WorldProgressData : IProgressData
    {
        public SavedTileData[] TileSavedData;
    }
}