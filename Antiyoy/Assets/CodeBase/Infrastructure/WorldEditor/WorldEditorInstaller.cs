using System;
using CodeBase.Dev.DebugWindow;
using CodeBase.Gameplay.Camera;
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
using CodeBase.Infrastructure.Project.Services.StateMachine;
using CodeBase.Infrastructure.Project.Services.StateMachine.Factory;
using CodeBase.Infrastructure.Project.Services.StaticData;
using CodeBase.Utilities.Zenject;
using CodeBase.WorldEditor;
using Sirenix.Serialization;

namespace CodeBase.Infrastructure.WorldEditor
{
    public class WorldEditorInstaller : SerializedMonoInstaller
    {
        [NonSerialized, OdinSerialize] private IStaticData[] _dataToProvide;

        public override void InstallBindings()
        {
            BindStateMachine();
            BindWorld();

            Container.Bind<IStaticDataProvider>().To<StaticDataProvider>().AsSingle().WithArguments(_dataToProvide);
            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<WorldEditorFactory>().AsSingle();
            Container.Bind<DebugWindowFactory>().AsSingle();
            Container.BindInterfacesTo<WorldEditorStartup>().AsSingle();
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
            Container.Bind<WorldProgressLoader>().AsSingle();
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