using CodeBase.Gameplay.Camera;
using CodeBase.Gameplay.World;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Terrain;
using CodeBase.Gameplay.World.Tile;
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
            Container.Bind<WorldFactory>().AsSingle();
            Container.Bind<CameraFactory>().AsSingle();
            Container.Bind<RegionFactory>().AsSingle();
            Container.Bind<TerrainFactory>().AsSingle();
        }
    }
}