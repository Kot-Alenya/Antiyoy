using CodeBase.Dev.DebugWindow;
using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World.Progress;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Entity;
using CodeBase.Gameplay.World.Terrain.Region;
using CodeBase.Gameplay.World.Terrain.Region.Factory;
using CodeBase.Gameplay.World.Terrain.Region.Rebuild;
using CodeBase.Gameplay.World.Terrain.Tile.Collection;
using CodeBase.Gameplay.World.Terrain.Tile.Factory;
using CodeBase.Gameplay.World.Version.Handler;
using CodeBase.Gameplay.World.Version.Recorder;
using CodeBase.Infrastructure.Project.Services.StateMachine;
using CodeBase.Infrastructure.Project.Services.StateMachine.Factory;
using CodeBase.WorldEditor;
using Zenject;

namespace CodeBase.Infrastructure.WorldEditor
{
    public class WorldEditorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStateMachine();
            BindWorld();

            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<WorldEditorFactory>().AsSingle();
            Container.Bind<DebugWindowFactory>().AsSingle();
        }

        private void BindStateMachine()
        {
            Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
            Container.Bind<IStateMachine>().To<StateMachine>().AsSingle();
        }

        private void BindWorld()
        {
            BindRegion();
            BindTile();

            Container.Bind<IEntityFactory>().To<EntityFactory>().AsSingle();
            Container.Bind<ITerrainFactory>().To<TerrainFactory>().AsSingle();
            Container.Bind<IWorldVersionHandler>().To<WorldVersionHandler>().AsSingle();
            Container.Bind<IWorldVersionRecorder>().To<WorldVersionRecorder>().AsSingle();
            Container.Bind<WorldProgressSaver>().AsSingle();
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