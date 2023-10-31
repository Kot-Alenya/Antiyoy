using System.Collections.Generic;
using CodeBase.Gameplay.Hex;
using CodeBase.Gameplay.Player.Data;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Gameplay.Terrain.Tile.Collection;
using CodeBase.Gameplay.Terrain.Tile.Data;
using CodeBase.Infrastructure.Project.Services.StaticData;

namespace CodeBase.Gameplay.Player
{
    public class PlayerTerrainSelectionView
    {
        private readonly ITileCollection _tileCollection;
        private readonly IStaticDataProvider _staticDataProvider;

        public PlayerTerrainSelectionView(ITileCollection tileCollection, IStaticDataProvider staticDataProvider)
        {
            _tileCollection = tileCollection;
            _staticDataProvider = staticDataProvider;
        }

        public void SelectRegion(RegionData region)
        {
            var playerStaticData = _staticDataProvider.Get<PlayerStaticData>();

            foreach (var tile in region.Tiles)
            foreach (var direction in HexPositionDirections.Directions)
            {
                var neighbourHex = tile.Hex + direction;
                var directionType = HexPositionDirections.GetDirectionType(direction);

                if (_tileCollection.TryGet(neighbourHex, out var neighbor))
                {
                    if (neighbor.Region.Type != region.Type)
                        tile.Instance.Borders[directionType].color = playerStaticData.SelectedRegionBorderColor;
                }
                else
                    tile.Instance.Borders[directionType].color = playerStaticData.SelectedRegionBorderColor;
            }
        }

        public void UnSelectRegion(RegionData region)
        {
            var playerStaticData = _staticDataProvider.Get<PlayerStaticData>();

            foreach (var tile in region.Tiles)
            foreach (var border in tile.Instance.Borders)
                border.Value.color = playerStaticData.UnSelectedRegionBorderColor;
        }

        public void SelectTiles(List<TileData> tiles)
        {
            var playerStaticData = _staticDataProvider.Get<PlayerStaticData>();

            foreach (var tile in tiles)
                tile.Instance.SpriteRenderer.color += playerStaticData.SelectedTileAdditionalColor;
        }

        public void UnSelectTiles(List<TileData> tiles)
        {
            var playerStaticData = _staticDataProvider.Get<PlayerStaticData>();

            foreach (var tile in tiles)
                tile.Instance.SpriteRenderer.color -= playerStaticData.SelectedTileAdditionalColor;
        }
    }
}