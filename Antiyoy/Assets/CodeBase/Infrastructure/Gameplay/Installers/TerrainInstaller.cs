using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Entity;
using CodeBase.Gameplay.World.Terrain.Entity.Operation;
using CodeBase.Gameplay.World.Terrain.Region;
using CodeBase.Gameplay.World.Terrain.Region.Rebuilder;
using CodeBase.Gameplay.World.Terrain.Tile;
using CodeBase.Gameplay.World.Terrain.Tile.Operation;
using Zenject;

namespace CodeBase.Infrastructure.Gameplay.Installers
{
    public abstract class TerrainInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindEntity();
            BindTile();
            BindRegion();

            Container.Bind<TerrainFactory>().AsSingle();
        }

        private void BindTile()
        {
            Container.Bind<TileFactory>().AsSingle();
            Container.Bind<TileConnector>().AsSingle();
            Container.Bind<TileVersionOperationFactory>().AsSingle();
            Container.Bind<CreateTileOperationHandler>().AsSingle();
            Container.Bind<DestroyTileOperationHandler>().AsSingle();
        }

        private void BindEntity()
        {
            Container.Bind<EntityFactory>().AsSingle();
            Container.Bind<EntityVersionOperationFactory>().AsSingle();
            Container.Bind<CreateEntityOperationHandler>().AsSingle();
            Container.Bind<DestroyEntityOperationHandler>().AsSingle();
        }

        private void BindRegion()
        {
            Container.Bind<RegionFactory>().AsSingle();
            Container.Bind<RegionConnector>().AsSingle();

            Container.Bind<RegionRebuilder>().AsSingle();
            Container.Bind<RegionJoiner>().AsSingle();
            Container.Bind<RegionSplitter>().AsSingle();
            Container.Bind<RegionIncomeRebuilder>().AsSingle();
        }
    }
}