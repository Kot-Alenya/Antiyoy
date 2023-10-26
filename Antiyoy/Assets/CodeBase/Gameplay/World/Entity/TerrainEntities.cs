using CodeBase.Gameplay.World.Entity.Data;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Tile.Data;

namespace CodeBase.Gameplay.World.Entity
{
    public class TerrainEntities : ITerrainEntities
    {
        private readonly ITerrainRegions _terrainRegions;

        public TerrainEntities(ITerrainRegions terrainRegions) => _terrainRegions = terrainRegions;

        public void Set(EntityData entity, TileData rootTile)
        {
            rootTile.Entity = entity;
            _terrainRegions.AddToRecalculateBuffer(rootTile.Region);
        }

        public void Remove(EntityData entity)
        {
            var rootTile = entity.RootTile;
            
            rootTile.Entity = null;
            _terrainRegions.AddToRecalculateBuffer(rootTile.Region);
        }
    }
}