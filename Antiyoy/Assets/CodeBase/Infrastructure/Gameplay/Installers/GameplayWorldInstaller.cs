using CodeBase.Gameplay.Progress;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Terrain.Entity;
using CodeBase.Gameplay.Terrain.Region;
using CodeBase.Gameplay.Terrain.Region.Factory;
using CodeBase.Gameplay.Terrain.Region.Rebuild;
using CodeBase.Gameplay.Terrain.Tile.Collection;
using CodeBase.Gameplay.Terrain.Tile.Factory;
using CodeBase.Gameplay.Version.Handler;
using CodeBase.Gameplay.Version.Recorder;
using Zenject;

namespace CodeBase.Infrastructure.Gameplay.Installers
{
    public class GameplayWorldInstaller : MonoInstaller
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
            Container.Bind<IWorldVersionHandler>().To<WorldVersionHandler>().AsSingle();
            Container.Bind<IWorldVersionRecorder>().To<WorldVersionRecorder>().AsSingle();
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