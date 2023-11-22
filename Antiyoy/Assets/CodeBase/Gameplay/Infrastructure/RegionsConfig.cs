using System;
using System.Collections.Generic;
using CodeBase.Gameplay.Region;
using UnityEngine;

namespace CodeBase.Gameplay.Infrastructure
{
    [Serializable]
    public class RegionsConfig
    {
        [SerializeField] private Color _neutral;
        [SerializeField] private Color _red;
        [SerializeField] private Color _blue;

        public Dictionary<RegionType, Color> Colors { get; private set; }

        public void Initialize()
        {
            Colors = new Dictionary<RegionType, Color>
            {
                { RegionType.Neutral, _neutral },
                { RegionType.Red, _red },
                { RegionType.Blue, _blue }
            };
        }
    }
}