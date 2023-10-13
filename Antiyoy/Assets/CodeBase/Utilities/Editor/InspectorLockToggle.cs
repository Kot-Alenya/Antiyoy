﻿using UnityEditor;

namespace CodeBase.Utilities.Editor
{
    public static class InspectorLockToggle
    {
        [MenuItem("Tools/Inspector Lock Toggle %l")]
        static void ToggleInspectorLock()
        {
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }
    }
}