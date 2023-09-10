using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Terrain.Data;
using CodeBase.Gameplay.Terrain.Tile;
using UnityEngine;

namespace _dev.Country
{
    //страна имеет: территории.

    public class CountryObject
    {
        public Color Color;
    }

    //в terrainFactory(?)
    public class CountryFactory
    {
        private readonly CapitalObjectStaticData _capitalPrefab;

        public CountryFactory(CapitalObjectStaticData capitalPrefab)
        {
            _capitalPrefab = capitalPrefab;
        }

        public void Create(TerrainObject terrain, HexCoordinates coordinates, Color color)
        {
            var capitalTile = terrain.Tiles.Get(coordinates);
            var country = new CountryObject();

            //country.Color = color;

            //capitalTile.SetCountry(country);
            //foreach (var connection in capitalTile.GetConnections())
            //    connection.ConnectedTile.SetCountry(country);

            CreateCapital(capitalTile);
        }

        //в отдельную фабрику(?)
        private void CreateCapital(TileObject tile)
        {
            Object.Instantiate(_capitalPrefab, tile.transform);
        }
    }

    //public class ConstructionObject
}

//регионы храняться в terrain.
//создаются вместе с terrain.