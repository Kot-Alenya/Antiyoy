using CodeBase.Gameplay.GameplayCamera;
using CodeBase.Gameplay.Terrain;
using CodeBase.Gameplay.Terrain.Tile;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
    public class GameplayInstaller : MonoInstaller
    {
        [SerializeField] private StaticData _staticData;

        public override void InstallBindings()
        {
            Container.Bind<StaticData>().FromInstance(_staticData).AsSingle();
            Container.Bind<GameplayStaticDataProvider>().AsSingle().WithArguments(_staticData.CameraConfig);
            Container.Bind<TileFactory>().AsSingle();
            Container.Bind<TerrainFactory>().AsSingle();
            Container.Bind<CameraFactory>().AsSingle();
        }
    }
}