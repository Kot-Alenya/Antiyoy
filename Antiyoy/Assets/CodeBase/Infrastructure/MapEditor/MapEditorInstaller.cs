using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Tile;
using CodeBase.MapEditor;
using Zenject;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindWorld();
            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<MapEditorFactory>().AsSingle();
        }

        private void BindWorld()
        {
            Container.Bind<TileFactory>().AsSingle();
            Container.Bind<RegionFactory>().AsSingle();
            Container.Bind<TerrainFactory>().AsSingle();
            Container.Bind<WorldFactory>().AsSingle();
        }
    }
}