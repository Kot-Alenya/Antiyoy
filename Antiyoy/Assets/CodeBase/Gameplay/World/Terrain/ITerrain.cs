using System.Collections.Generic;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Terrain.Entity.Data;
using CodeBase.Gameplay.World.Terrain.Region.Data;
using CodeBase.Gameplay.World.Terrain.Tile;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain
{
    public interface ITerrain : IEnumerable<TileData>
    {
        public Vector2Int Size { get; }

        public void Initialize(TileArray tileArray);

        public bool IsInTerrain(HexPosition hex);

        public void CreateTile(HexPosition hex, RegionType regionType);

        public void DestroyTile(TileData tile);

        public void CreateUnit(TileData rootTile, UnitType unitType);

        public void DestroyUnit(UnitData unit);

        public TileData GetTile(HexPosition hex);

        public bool TryGetTile(HexPosition hex, out TileData tile);
    }
}