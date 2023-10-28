using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Terrain.Region.Factory;
using CodeBase.Gameplay.Terrain.Tile.Factory;
using Zenject;

namespace CodeBase.Infrastructure.Gameplay
{
    public class GameplayInstaller : MonoInstaller
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