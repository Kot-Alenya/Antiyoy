using UnityEngine;

namespace CodeBase.Dev.Tests
{
    public static class TestsCreate
    {
        public static TestStaticData TestConfig() => Resources.Load<TestStaticData>("TestsConfig");
    }
}