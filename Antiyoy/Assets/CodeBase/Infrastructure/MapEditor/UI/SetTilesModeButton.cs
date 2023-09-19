﻿using CodeBase.Gameplay.Terrain.Region;
using CodeBase.Gameplay.Terrain.Region.Data;
using CodeBase.Utilities.UI;
using UnityEngine;

namespace CodeBase.Infrastructure.MapEditor.UI
{
    public class SetTilesModeButton : ButtonBase, IMapEditorUIElement
    {
        [SerializeField] private RegionType _tilesRegionType;

        private MapEditorController _controller;

        public void Constructor(MapEditorController controller) => _controller = controller;

        private protected override void OnClick() => _controller.SetTilesMode(_tilesRegionType);
    }
}