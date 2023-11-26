using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CodeBase.Gameplay.Region
{
    public class RegionFactory
    {
        private readonly IInstantiator _instantiator;

        private RegionFactory(IInstantiator instantiator) => _instantiator = instantiator;

        public RegionEntitiesCollection CreateEntitiesCollection(List<int> entities)
        {
            var collection = _instantiator.Instantiate<RegionEntitiesCollection>(new object[]
            {
                Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f),
                entities
            });

            collection.Initialize();

            return collection;
        }

        public RegionController CreateControllerAndRegister(RegionType type, RegionEntitiesCollection entities)
        {
            var controller = _instantiator.Instantiate<RegionController>(new object[]
            {
                type,
                entities
            });

            return controller;
        }

        public void Destroy(RegionController controller)
        {
            controller.Entities.Clear();
        }
    }
}