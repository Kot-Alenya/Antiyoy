namespace CodeBase.Dev.DebugWindow
{
    public class DebugWindowController : IDebugWindowController
    {
        public DebugWindowController(DebugWindow window) => Window = window;

        public DebugWindow Window { get; }
    }
}