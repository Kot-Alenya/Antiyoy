using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region.Data;
using CodeBase.Gameplay.World.Terrain;
using NUnit.Framework;
using Zenject;

namespace CodeBase.Dev.Tests.World
{
    [TestFixture]
    public class TerrainTests : ZenjectUnitTestFixture
    {
        private IWorldTerrainController _terrain;

        [SetUp]
        public void SetUp() => _terrain = TestsSetup.Terrain(Container);

        [Test]
        public void WhenCreateTile_AndTerrainIsEmpty_ThenTerrainHas1Tile()
        {
            //Arrange
            var hex = new HexPosition(0, 0);

            //Act
            _terrain.TryCreateTile(hex, RegionType.Neutral);

            //Assert
            Assert.IsTrue(_terrain.TryGetTile(hex, out _));
        }
    }
}