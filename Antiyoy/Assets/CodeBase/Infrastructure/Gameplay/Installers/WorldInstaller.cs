using CodeBase.Dev.TEMPO;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Progress;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Region;
using CodeBase.Gameplay.World.Terrain.Region.Rebuilder;
using CodeBase.Gameplay.World.Terrain.Region.Rebuilder.Geometry;
using CodeBase.Gameplay.World.Terrain.Tile;
using CodeBase.Gameplay.World.Terrain.Tile.Operation;
using CodeBase.Gameplay.World.Terrain.Unit;
using CodeBase.Gameplay.World.Terrain.Unit.Data;
using CodeBase.Gameplay.World.Terrain.Unit.Operation;
using CodeBase.Gameplay.World.Version;
using Zenject;

namespace CodeBase.Infrastructure.Gameplay.Installers
{
    public class WorldInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindTerrain();
            BindVersion();
            BindProgress();

            Container.Bind<WorldFactory>().AsSingle();
        }

        private void BindTerrain()
        {
            BindUnit();
            BindTile();
            BindRegion();

            Container.Bind<TerrainFactory>().AsSingle();
            Container.Bind<ITerrain>().To<TerrainObject>().AsSingle();
        }

        private void BindTile()
        {
            Container.Bind<TileFactory>().AsSingle();
            Container.Bind<TileConnector>().AsSingle();
            Container.Bind<TileVersionOperationFactory>().AsSingle();
            Container.Bind<CreateTileOperationHandler>().AsSingle();
            Container.Bind<DestroyTileOperationHandler>().AsSingle();
        }

        private void BindUnit()
        {
            Container.Bind<UnitFactory>().AsSingle();
            Container.Bind<UnitStaticDataHelper>().AsSingle();
            Container.Bind<UnitVersionOperationFactory>().AsSingle();
            Container.Bind<CreateUnitOperationHandler>().AsSingle();
            Container.Bind<DestroyUnitOperationHandler>().AsSingle();
        }

        private void BindRegion()
        {
            Container.Bind<RegionFactory>().AsSingle();
            Container.Bind<RegionConnector>().AsSingle();

            Container.Bind<RegionGeometryRebuilder>().AsSingle();
            Container.Bind<RegionJoiner>().AsSingle();
            Container.Bind<RegionSplitter>().AsSingle();
            Container.Bind<RegionIncomeRebuilder>().AsSingle();
            Container.Bind<RegionCoinsRebuilder>().AsSingle();
        }

        private void BindVersion()
        {
            Container.Bind<WorldVersionManager>().AsSingle();
            Container.Bind<WorldVersionRecorder>().AsSingle();
        }

        private void BindProgress()
        {
            Container.Bind<WorldProgressLoader>().AsSingle();
            Container.Bind<WorldProgressSaver>().AsSingle();
        }
    }
}