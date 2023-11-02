namespace CodeBase.Gameplay.World.Version
{
    public class WorldVersionManager
    {
        private readonly WorldVersionRecorder _worldVersionRecorder;

        public WorldVersionManager(WorldVersionRecorder worldVersionRecorder) =>
            _worldVersionRecorder = worldVersionRecorder;

        public void ReturnBack()
        {
            if (!_worldVersionRecorder.TryGetPreviousRecord(out var record))
                return;

            for (var i = record.Length - 1; i >= 0; i--)
                record[i].Handler.Revert(record[i]);
        }

        public void ReturnNext()
        {
            if (!_worldVersionRecorder.TryGetNextRecord(out var record))
                return;
            
            for (var i = 0; i < record.Length; i++)
                record[i].Handler.Apply(record[i]);
        }
    }
}