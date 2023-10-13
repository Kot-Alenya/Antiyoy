using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Tile;
using Zenject;

namespace CodeBase.Infrastructure.MapEditor
{
    public class MapEditorInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TileFactory>().AsSingle();
            Container.Bind<WorldFactory>().AsSingle();
            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<RegionFactory>().AsSingle();
            Container.Bind<TerrainFactory>().AsSingle();
            Container.Bind<MapEditorFactory>().AsSingle();
        }
    }
}