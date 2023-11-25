using SevenBoldPencil.EasyEvents;

namespace CodeBase.Gameplay.Region.Ecs.Components
{
    public struct RegionRecalculateEvent : IEventReplicant
    {
        public RegionController Controller;
    }
}