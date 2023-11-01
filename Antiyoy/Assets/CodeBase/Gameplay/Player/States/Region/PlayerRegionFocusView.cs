using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Collection;
using CodeBase.Infrastructure.Services.StaticData;

namespace CodeBase.Gameplay.Player.States.Region
{
    public class PlayerRegionFocusView
    {
        private readonly ITileCollection _tileCollection;
        private readonly IStaticDataProvider _staticDataProvider;

        public PlayerRegionFocusView(ITileCollection tileCollection, IStaticDataProvider staticDataProvider)
        {
            _tileCollection = tileCollection;
            _staticDataProvider = staticDataProvider;
        }

        public void FocusRegion(RegionData region)
        {
            var playerStaticData = _staticDataProvider.Get<PlayerStaticData>();

            foreach (var tile in region.Tiles)
            foreach (var direction in HexPositionDirectionUtilities.Directions)
            {
                var neighbourHex = tile.Hex + direction;
                var directionType = HexPositionDirectionUtilities.GetDirectionType(direction);

                if (_tileCollection.TryGet(neighbourHex, out var neighbor))
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
            var playerStaticData = _staticDataProvider.Get<PlayerStaticData>();

            foreach (var tile in _tileCollection)
            foreach (var border in tile.Instance.Borders)
                border.Value.color = playerStaticData.UnSelectedRegionBorderColor;
        }
    }
}