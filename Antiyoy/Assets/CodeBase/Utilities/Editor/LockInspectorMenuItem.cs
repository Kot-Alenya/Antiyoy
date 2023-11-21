﻿using UnityEditor;

namespace CodeBase.Utilities.Editor
{
    public static class LockInspectorMenuItem
    {
        // taken from: http://answers.unity3d.com/questions/282959/set-inspector-lock-by-code.html
        [MenuItem("Tools/Toggle Inspector Lock %l")] // Ctrl + L
        static void ToggleInspectorLock()
        {
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }
    }
}