namespace CodeBase.Dev.TEMPO.Region.Rebuild
{
    public interface IRegionRebuilder
    {
        void AddToRebuildBuffer(RegionData region);
        void RebuildFromBufferAndClearBuffer();
        void RebuildIncome(RegionData region);
    }
}