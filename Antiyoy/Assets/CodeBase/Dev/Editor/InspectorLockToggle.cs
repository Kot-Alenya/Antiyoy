﻿using UnityEditor;

namespace CodeBase.Dev.Editor
{
    public static class InspectorLockToggle
    {
        [MenuItem("Tools/Inspector Lock Toggle %l")]
        private static void ToggleInspectorLock()
        {
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }
    }
}