namespace CodeBase.Dev.Tests
{
    public static class TestsSetup
    {
        /*
        public static void StaticDataProvider(DiContainer container)
        {
            var testConfig = TestsCreate.TestConfig();

            container.Bind<IStaticDataProvider>().To<StaticDataProvider>().AsSingle()
                .WithArguments(testConfig.ProjectStaticData.StaticDataToBind);
        }

        public static ITerrain Terrain(DiContainer container)
        {
            StaticDataProvider(container);

            container.Bind<TileFactory>().AsSingle();
            container.Bind<RegionFactory>().AsSingle();
            container.Bind<TerrainFactory>().AsSingle();

            return container.Resolve<TerrainFactory>().Create();
        }
        */
    }
}