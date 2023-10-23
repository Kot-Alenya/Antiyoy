﻿using CodeBase.Infrastructure.Bootstrap;
using UnityEngine;

namespace CodeBase.Dev.Tests
{
    [CreateAssetMenu(menuName = "Configurations/Tests", fileName = "TestsConfig", order = 0)]
    public class TestStaticData : ScriptableObject
    {
        public ProjectStaticData ProjectStaticData;
    }
}