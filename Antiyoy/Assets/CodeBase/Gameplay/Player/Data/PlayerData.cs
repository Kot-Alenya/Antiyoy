using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;

namespace CodeBase.Gameplay.Player.Data
{
    public class PlayerData
    {
        public readonly PlayerPrefabData Instance;
        public readonly RegionType RegionType;

        public RegionData SelectedRegion;
        public UnitData SelectedUnit;
        
        public PlayerData(PlayerPrefabData instance, RegionType regionType)
        {
            Instance = instance;
            RegionType = regionType;
        }
    }
}