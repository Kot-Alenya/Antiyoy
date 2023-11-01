using CodeBase.Gameplay.World.Progress;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Entity;
using CodeBase.Gameplay.World.Terrain.Region;
using CodeBase.Gameplay.World.Terrain.Region.Factory;
using CodeBase.Gameplay.World.Terrain.Region.Rebuild;
using CodeBase.Gameplay.World.Terrain.Tile.Collection;
using CodeBase.Gameplay.World.Terrain.Tile.Factory;
using CodeBase.Gameplay.World.Version;
using Zenject;

namespace CodeBase.Infrastructure.Gameplay.Installers
{
    public class WorldInstaller : MonoInstaller
    {
        public override void InstallBindings() => BindWorld();

        private void BindWorld()
        {
            BindRegion();
            BindTile();
            BindVersion();
            BindProgress();

            Container.Bind<IEntityFactory>().To<EntityFactory>().AsSingle();
            Container.Bind<ITerrainFactory>().To<TerrainFactory>().AsSingle();
            Container.Bind<RegionCoinsCounter>().AsSingle();
        }

        private void BindProgress()
        {
            Container.Bind<WorldProgressSaver>().AsSingle();
            Container.Bind<WorldProgressLoader>().AsSingle();
        }

        private void BindVersion()
        {
            Container.Bind<IVersionHandler>().To<WorldVersionHandler>().AsSingle();
            Container.Bind<IVersionRecorder>().To<WorldVersionRecorder>().AsSingle();
        }

        private void BindRegion()
        {
            Container.Bind<IRegionFactory>().To<RegionFactory>().AsSingle();
            Container.Bind<RegionCollection>().To<RegionCollection>().AsSingle();
            Container.Bind<IRegionRebuilder>().To<RegionRebuilder>().AsSingle();
            Container.Bind<RegionSplitter>().AsSingle();
            Container.Bind<RegionJoiner>().AsSingle();
            Container.Bind<RegionIncomeRebuilder>().AsSingle();
        }

        private void BindTile()
        {
            Container.Bind<ITileCollection>().To<TileArray>().AsSingle();
            Container.Bind<ITileFactory>().To<TileFactory>().AsSingle();
        }
    }
}