﻿namespace CodeBase.MapEditor.UI
{
    public class MapEditorReturnBackButton : MapEditorButtonBase
    {
        private protected override void OnClick() => MapEditorController.ReturnBack();
    }
}