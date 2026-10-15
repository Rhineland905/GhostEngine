using GhostEngine.DesktopGL.Editor.Model;

namespace GhostEngine.DesktopGL.Editor
{
    // Editor-wide state that is not part of the UI (survives UI rebuilds on theme change)
    internal static class EditorState
    {
        public const int DefaultLevelWidth = 64;
        public const int DefaultLevelHeight = 32;

        // The level open right now; replaced later by File > New / Open
        public static Level CurrentLevel { get; set; }
    }
}
