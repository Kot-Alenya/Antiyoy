﻿using System.Collections.Generic;
using CodeBase.Infrastructure.Project.Services.StaticData.Data;
using Sirenix.Serialization;
using UnityEngine;

namespace CodeBase.Gameplay.Terrain.Entity.Data
{
    [CreateAssetMenu(menuName = "Configurations/Enitities", fileName = "EntitiesPresetsCollection", order = 0)]
    public class EntitiesPresetsCollection : ScriptableObjectStaticData
    {
        [OdinSerialize] public Dictionary<EntityType, EntityPresetData> Entities;
    }
}