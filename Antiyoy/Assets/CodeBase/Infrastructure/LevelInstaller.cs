using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Terrain.Region;
using CodeBase.Gameplay.Terrain.Tile;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField] private StaticData _staticData;

        public override void InstallBindings()
        {
            Container.Bind<StaticData>().FromInstance(_staticData).AsSingle();
            Container.Bind<TileFactory>().AsSingle();
            Container.Bind<TerrainFactory>().AsSingle();
            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<RegionFactory>().AsSingle();
        }
    }
}