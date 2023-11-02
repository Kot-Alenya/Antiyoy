using CodeBase.Gameplay.World.Terrain.Region.Data;

namespace CodeBase.Gameplay.Player.Data
{
    public class PlayerData
    {
        public readonly PlayerPrefabData Instance;
        public readonly RegionType RegionType;
        public RegionData CurrentRegion;
        public int CoinsCount;

        public PlayerData(PlayerPrefabData instance, RegionType regionType)
        {
            Instance = instance;
            RegionType = regionType;
        }
    }
}