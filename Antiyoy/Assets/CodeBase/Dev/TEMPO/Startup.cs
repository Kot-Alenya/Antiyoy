using Leopotam.EcsLite;
using UnityEngine;

namespace CodeBase.Dev.TEMPO
{
    class Startup : MonoBehaviour
    {
        EcsWorld _world;
        IEcsSystems _systems;

        void Start () {
            var world = new EcsWorld ();
            _systems = new EcsSystems (world);

            _systems
#if UNITY_EDITOR
                // Регистрируем отладочные системы по контролю за состоянием каждого отдельного мира:
                // .Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ("events"))
                //.Add (new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem ())
                // Регистрируем отладочные системы по контролю за текущей группой систем. 
                //.Add (new Leopotam.EcsLite.UnityEditor.EcsSystemsDebugSystem ())
#endif
                .Init ();
        }

        void Update () {
            _systems?.Run ();
        }

        void OnDestroy () {
            _systems?.Destroy ();
            _systems?.GetWorld ()?.Destroy ();
            _systems = null;
        }
    }
}