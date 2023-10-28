using System.Collections.Generic;
using CodeBase.Infrastructure.Project.Services.StaticData;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Gameplay.World.Terrain.Entity.Data
{
    [CreateAssetMenu(menuName = "Configurations/Enitities", fileName = "EntitiesPresetsCollection", order = 0)]
    public class EntitiesPresetsCollection : SerializedScriptableObject, IStaticData
    {
        [OdinSerialize] public Dictionary<EntityType, EntityPresetData> Entities;
    }
}