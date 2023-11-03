using System;
using CodeBase.Gameplay.World.Terrain.Unit.Data;

namespace CodeBase.Gameplay.World.Progress.SavedData
{
    [Serializable]
    public struct SavedUnitData
    {
        public UnitType Type;
        public bool IsCanMove;
    }
}