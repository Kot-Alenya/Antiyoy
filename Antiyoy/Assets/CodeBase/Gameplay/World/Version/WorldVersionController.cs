using CodeBase.Gameplay.World.Version.Modules;
using CodeBase.Gameplay.World.Version.Operation;

namespace CodeBase.Gameplay.World.Version
{
    public class WorldVersionController : IWorldVersionController
    {
        private readonly VersionHandler _handler;
        private readonly VersionRecorder _recorder;

        public WorldVersionController(VersionRecorder recorder, VersionHandler handler)
        {
            _recorder = recorder;
            _handler = handler;
        }

        public void Record(params IWorldOperationData[] changes) => _recorder.Record(changes);

        public void AddToBuffer(IWorldOperationData change) => _recorder.Buffer.Add(change);

        public void RecordFromBufferAndClearBuffer()
        {
            _recorder.Record(_recorder.Buffer.ToArray());
            _recorder.Buffer.Clear();
        }

        public void ClearRecords() => _recorder.Clear();

        public void ReturnWorldBack()
        {
            if (!_recorder.TryBack(out var data))
                return;

            for (var i = data.Length - 1; i >= 0; i--)
                _handler.Revert(data[i]);
        }

        public void ReturnWorldNext()
        {
            if (!_recorder.TryNext(out var data))
                return;

            for (var i = 0; i < data.Length; i++)
                _handler.Apply(data[i]);
        }
    }
}