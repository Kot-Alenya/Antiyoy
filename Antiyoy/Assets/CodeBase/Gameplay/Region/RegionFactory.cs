namespace CodeBase.Gameplay.Region.Ecs
{
    public class RegionFactory
    {
        public RegionController CreateController(RegionType regionType)
        {
            return new RegionController();
        }
    }
}