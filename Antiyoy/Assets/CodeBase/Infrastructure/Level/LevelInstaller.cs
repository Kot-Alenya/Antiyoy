using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Terrain.Region.Factory;
using CodeBase.Gameplay.World.Terrain.Tile.Factory;
using Zenject;

namespace CodeBase.Infrastructure.Level
{
    public class LevelInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<TileFactory>().AsSingle();
            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<RegionFactory>().AsSingle();
            Container.Bind<TerrainFactory>().AsSingle();
        }
    }
}