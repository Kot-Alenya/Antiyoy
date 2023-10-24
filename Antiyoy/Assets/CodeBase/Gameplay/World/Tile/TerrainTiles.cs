using CodeBase.Gameplay.World.Entity;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Tile
{
    public class TerrainTiles : ITerrainTiles
    {
        private readonly TileCollection _tileCollection;
        private readonly ITerrainRegions _terrainRegions;
        private readonly IEntityFactory _entityFactory;

        public TerrainTiles(TileCollection tileCollection, ITerrainRegions terrainRegions, IEntityFactory entityFactory,
            Transform tilesRoot)
        {
            _tileCollection = tileCollection;
            _terrainRegions = terrainRegions;
            _entityFactory = entityFactory;
            TilesRoot = tilesRoot;
        }

        public Transform TilesRoot { get; }

        public bool IsInTerrain(HexPosition hex) => _tileCollection.IsInArraySize(hex);

        public void Set(TileData tile, HexPosition hex, RegionType regionType)
        {
            _tileCollection.Set(tile, hex);
            TileUtilities.ConnectWithNeighbors(tile, _tileCollection);
            _terrainRegions.AddToRegion(tile, regionType);
        }

        public void Remove(TileData tile)
        {
            _entityFactory.TryDestroy(tile.Hex);
            TileUtilities.DisconnectFromNeighbors(tile);
            _tileCollection.Remove(tile.Hex);
            _terrainRegions.RemoveFromRegion(tile);
        }

        public TileData Get(HexPosition hex) => _tileCollection.Get(hex);

        public bool TryGet(HexPosition hex, out TileData tile) => _tileCollection.TryGet(hex, out tile);
    }
}