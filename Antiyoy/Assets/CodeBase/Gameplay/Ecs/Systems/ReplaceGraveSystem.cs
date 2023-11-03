using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Region;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using Leopotam.EcsLite;

namespace CodeBase.Gameplay.Ecs.Systems
{
    public class ReplaceGraveSystem : IEcsRunSystem
    {
        private readonly RegionFactory _regionFactory;
        private readonly ITerrain _terrain;

        public ReplaceGraveSystem(RegionFactory regionFactory, ITerrain terrain)
        {
            _regionFactory = regionFactory;
            _terrain = terrain;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var region in _regionFactory.Regions)
            foreach (var tile in region.Tiles)
                if (tile.Unit.Type == UnitType.Grave)
                    ReplaceGrave(tile);
        }

        private void ReplaceGrave(TileData tile)
        {
            _terrain.DestroyUnit(tile.Unit);
            _terrain.CreateUnit(tile, UnitType.Pine, false);
        }
    }
}