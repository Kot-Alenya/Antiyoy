using CodeBase.Gameplay.Terrain.Tile.Data;
using CodeBase.Utilities.UI;
using TMPro;
using UnityEngine;

namespace CodeBase.Dev.DebugWindow
{
    public class DebugWindow : WindowBase
    {
        [SerializeField] private TextMeshProUGUI _windowText;

        public override void Open()
        {
            gameObject.SetActive(true);
            base.Open();
        }

        public override void Close()
        {
            gameObject.SetActive(false);
            base.Close();
        }

        public void UpdateInformation(TileData tile)
        {
            var tabulation = "      ";

            _windowText.text = $"Info:\n" +
                               $"Hex: {tile.Hex}\n" +
                               $"Neighbours:\n" +
                               $"{GetNeighborsText(tile, tabulation)}\n" +
                               $"Region:\n" +
                               $"{GetRegionText(tile, tabulation)}";
        }

        private string GetNeighborsText(TileData tile, string tabulation)
        {
            var result = string.Empty;

            result += tabulation;

            for (var i = 0; i < tile.Neighbors.Count; i++)
            {
                var neighbour = tile.Neighbors[i];
                result += $"{neighbour.Hex}, ";

                if (i != tile.Neighbors.Count - 1)
                {
                    if ((i + 1) % 3 == 0)
                    {
                        result += "\n";
                        result += tabulation;
                    }
                }
            }

            return result;
        }

        private string GetRegionText(TileData tile, string tabulation)
        {
            if (tile.Region == default)
                return "NONE";

            var result = tabulation + $"Id: {tile.Region.Id}\n" +
                         tabulation + $"Size: {tile.Region.Tiles.Count}\n" +
                         tabulation + $"Income: {tile.Region.Income}\n" +
                         tabulation + $"Type: {tile.Region.Type}";

            return result;
        }
    }
}