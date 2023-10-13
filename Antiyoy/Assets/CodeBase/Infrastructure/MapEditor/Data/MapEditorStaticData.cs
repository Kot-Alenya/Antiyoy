using CodeBase.Gameplay.World.Terrain;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

namespace CodeBase.Infrastructure.MapEditor.Data
{
    [CreateAssetMenu(menuName = "Configurations/MapEditor", fileName = "MapEditorConfig", order = 0)]
    public class MapEditorStaticData : ScriptableObject, IStaticData
    {
        public MapEditorPrefabData Prefab;
    }
}