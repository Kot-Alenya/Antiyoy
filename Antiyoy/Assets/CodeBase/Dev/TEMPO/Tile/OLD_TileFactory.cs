using CodeBase.Dev.TEMPO.Region;
using CodeBase.Gameplay.World.Hex;
using CodeBase.Gameplay.World.Region;
using CodeBase.Gameplay.World.Terrain.Region;
using CodeBase.Gameplay.World.Terrain.Tile.Collection;
using CodeBase.Gameplay.World.Terrain.Tile.Data;
using CodeBase.Gameplay.World.Terrain.Tile.Factory;
using CodeBase.Gameplay.World.Tile.Data;
using CodeBase.Infrastructure.Services.StaticData;
using UnityEngine;

public class OLD_TileFactory : ITileFactory
{
    private readonly IStaticDataProvider _staticDataProvider;
    private readonly ITileCollection _tileCollection;
    private readonly RegionCollection _regionCollection;
    private Transform _tileRoot;

    public OLD_TileFactory(IStaticDataProvider staticDataProvider, ITileCollection tileCollection,
        RegionCollection regionCollection)
    {
        _staticDataProvider = staticDataProvider;
        _tileCollection = tileCollection;
        _regionCollection = regionCollection;
    }

    public void Initialize(Transform tileRoot) => _tileRoot = tileRoot;

    public void Create(HexPosition hex, RegionType regionType)
    {
        var instance = CreateInstance(hex, _tileRoot);
        var tile = new TileData(instance, hex);

        _tileCollection.Set(tile, hex);
        _regionCollection.AddTileToRegion(tile, regionType);
    }

    public void Destroy(TileData tile)
    {
        _tileCollection.Remove(tile.Hex);
        _regionCollection.RemoveTileFromRegion(tile);

        Object.Destroy(tile.Instance.gameObject);
    }

    public void Destroy(HexPosition hex) => Destroy(_tileCollection.Get(hex));

    public bool TryCreate(HexPosition hex, RegionType regionType)
    {
        if (!_tileCollection.TryGet(hex, out _))
        {
            Create(hex, regionType);
            return true;
        }

        return false;
    }

    public bool TryDestroy(HexPosition hex)
    {
        if (_tileCollection.TryGet(hex, out var tile))
        {
            Destroy(tile);
            return true;
        }

        return false;
    }

    private TilePrefabData CreateInstance(HexPosition hex, Transform root)
    {
        var position = HexMath.ToWorldPosition(hex);
        var tileStaticData = _staticDataProvider.Get<TileStaticData>();
        var instance = Object.Instantiate(tileStaticData.Prefab, root);
        var transform = instance.transform;

        transform.position = new Vector3(position.x, position.y, transform.position.z);

        return instance;
    }
}