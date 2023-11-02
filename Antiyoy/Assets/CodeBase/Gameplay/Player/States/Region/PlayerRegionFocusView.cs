using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Infrastructure.Services.StaticData;

namespace CodeBase.Gameplay.Player.States.Region
{
    public class PlayerRegionFocusView
    {
        private readonly ITerrain _terrain;
        private readonly IStaticDataProvider _staticDataProvider;

        public PlayerRegionFocusView(IStaticDataProvider staticDataProvider, ITerrain terrain)
        {
            _terrain = terrain;
            _staticDataProvider = staticDataProvider;
        }

        public void FocusRegion(RegionData region)
        {
            var playerStaticData = _staticDataProvider.Get<PlayerConfig>();

            foreach (var tile in region.Tiles)
            foreach (var direction in HexPositionDirectionUtilities.Directions)
            {
                var neighbourHex = tile.Hex + direction;
                var directionType = HexPositionDirectionUtilities.GetDirectionType(direction);

                if (_terrain.TryGetTile(neighbourHex, out var neighbor))
                {
                    if (neighbor.Region.Type != region.Type)
                        tile.Instance.Borders[directionType].color = playerStaticData.SelectedRegionBorderColor;
                }
                else
                    tile.Instance.Borders[directionType].color = playerStaticData.SelectedRegionBorderColor;
            }
        }

        public void UnFocusAllRegion()
        {
            var playerStaticData = _staticDataProvider.Get<PlayerConfig>();

            foreach (var tile in _terrain)
            foreach (var border in tile.Instance.Borders)
                border.Value.color = playerStaticData.UnSelectedRegionBorderColor;
        }
    }
}