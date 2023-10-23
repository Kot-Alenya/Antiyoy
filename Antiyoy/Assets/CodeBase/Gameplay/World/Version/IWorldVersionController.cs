using CodeBase.Gameplay.World.Version.Operation;

namespace CodeBase.Gameplay.World.Version
{
    public interface IWorldVersionController
    {
        public void Record(params IWorldOperationData[] changes);

        public void AddToBuffer(IWorldOperationData change);

        public void RecordFromBufferAndClearBuffer();

        public void ClearRecords();

        public void ReturnWorldBack();

        public void ReturnWorldNext();
    }
}