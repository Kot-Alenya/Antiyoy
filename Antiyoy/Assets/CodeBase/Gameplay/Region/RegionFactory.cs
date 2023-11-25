using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Region
{
    public class RegionFactory
    {
        private readonly IInstantiator _instantiator;

        private RegionFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public RegionController CreateController(RegionType type, List<int> entities)
        {
            var controller = _instantiator.Instantiate<RegionController>(new object[]
            {
                type,
                Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f),
                entities
            });

            controller.Initialize();

            return controller;
        }

        public RegionController CreateControllerAndRegister(RegionType type)
        {
            return CreateController(type, new List<int>());
        }

        public void Destroy(RegionController controller)
        {
            controller.Entities.Clear();
        }
    }
}