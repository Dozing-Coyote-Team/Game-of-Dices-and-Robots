using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
    public static event Action OnGridReady;

    public GameObject[,] TileList{ get; private set; }
    public int TilePerRow { get => _tilePerRow; }
    public float TileHeight { get => _tileHeight; }

    [Header("Grid References")]
    [SerializeField]
    private GameObject _gridObj = null;
    [SerializeField]
    private GameObject _gridTilePrefab = null;
    [SerializeField]
    private Material _gridMaterial = null;

    [Header("Grid Settings")]
    [Min(3)]
    [SerializeField]
    private int _tilePerRow = 3;
    [SerializeField]
    private float _tileHeight = 0.5f;

    private float _tileGap = 0;
    private int _tileCount = 0;
    private int _gridSize = 0;

    public bool CheckInArea(Vector2Int index, int radius, Type type)
    {
        Vector2Int currentIndex = new Vector2Int(index.x - radius , index.y - radius);
        int startX = currentIndex.x;
        GameObject currentTileObj;
        radius *= radius;
        radius++;
        for(int y = 0; y < radius; y++)
        {
            for(int x = 0; x < radius; x++)
            {
                currentTileObj = GetObjectAt(currentIndex);

                if (currentTileObj != null && currentTileObj.GetComponent(type))
                    return true;

                currentIndex.x++;
            }
            currentIndex.x = startX;
            currentIndex.y++;
            
        }
        return false;
    }

    public bool CheckInArea(Vector2Int index, int radius, Type type, out List<Vector2Int> objIndexes)
    {
        Vector2Int currentIndex = new Vector2Int(index.x - radius, index.y - radius);
        int startX = currentIndex.x;
        GameObject currentTileObj;
        radius *= radius;
        radius++;
        objIndexes = new List<Vector2Int>();
        for (int y = 0; y < radius; y++)
        {
            for (int x = 0; x < radius; x++)
            {
                currentTileObj = GetObjectAt(currentIndex);

                if (currentTileObj != null && currentTileObj.GetComponent(type))
                    objIndexes.Add(currentIndex);

                currentIndex.x++;
            }
            currentIndex.x = startX;
            currentIndex.y++;

        }

        if(objIndexes.Count > 0)
            return true;
        else
            return false;
    }

    public Dictionary<Vector2Int, GameObject> GetNeighboursObj(Vector2Int index)
    {
        Dictionary<Vector2Int, GameObject> result = new Dictionary<Vector2Int, GameObject>();

        Vector2Int curIndex = new Vector2Int(index.x + 1, index.y);
        GameObject obj = GetObjectAt(curIndex);
        if(obj != null)
            result.Add(curIndex, obj);

        curIndex = new Vector2Int(index.x - 1, index.y);
        obj = GetObjectAt(curIndex);
        if (obj != null)
            result.Add(curIndex, obj);

        curIndex = new Vector2Int(index.x, index.y + 1);
        obj = GetObjectAt(curIndex);
        if (obj != null)
            result.Add(curIndex, obj);

        curIndex = new Vector2Int(index.x, index.y - 1);
        obj = GetObjectAt(curIndex);
        if (obj != null)
            result.Add(curIndex, obj);

        return result;
    }

    public List<Enemy> GetNeighbourEnemies(Vector2Int index)
    {
        List<Enemy> result = new List<Enemy>();
        Vector2Int curIndex = new Vector2Int(index.x + 1, index.y);

        Enemy enemyObj;
        GameObject obj = GetObjectAt(curIndex);
        if (obj != null && obj.TryGetComponent(out enemyObj))
            result.Add(enemyObj);

        curIndex = new Vector2Int(index.x - 1, index.y);
        obj = GetObjectAt(curIndex);
        if (obj != null && obj.TryGetComponent(out enemyObj))
            result.Add(enemyObj);

        curIndex = new Vector2Int(index.x, index.y + 1);
        obj = GetObjectAt(curIndex);
        if (obj != null && obj.TryGetComponent(out enemyObj))
            result.Add(enemyObj);

        curIndex = new Vector2Int(index.x, index.y - 1);
        obj = GetObjectAt(curIndex);
        if (obj != null && obj.TryGetComponent(out enemyObj))
            result.Add(enemyObj);

        return result;
    }

    public GameObject GetObjectAt(Vector2Int index)
    {
        GridTile result = GetTileAt(index);
        if (result != null)
            return result.TileObject;

        return null;
    }

    public GridTile GetTileAt(Vector2Int index)
    {
        if (!ExistTileAt(index))
            return null;

        return TileList[index.x, index.y].GetComponent<GridTile>();
    }

    public bool ExistTileAt(Vector2Int index)
    {
        if (index.x < 0 || index.y < 0 || index.x >= _tilePerRow || index.y >= _tilePerRow)
            return false;

        return true;
    }

    public void AssignEmptyTiles()
    {
        for (int x = 0; x < TileList.GetLength(0); x++)
        {
            for (int y = 0; y < TileList.GetLength(1); y++)
            {
                TileList[x, y].GetComponent<GridTile>().AutoSetEmpty();
            }
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _gridMaterial.SetInt("_GridSize", _tilePerRow);
        InitGrid();
    }

    private void InitGrid()
    {
        int rowCount = -1;
        int colCount = 0;

        _gridSize = _gridMaterial.GetInt("_GridSize");
        _tileCount = (int)Mathf.Pow(_gridSize, 2);
        _tileGap = _gridObj.transform.localScale.x / _gridSize;

        TileList = new GameObject[_gridSize, _gridSize];

        bool isRowOdd = _tileCount % 2 != 0;

        // Calc the initialPos of the first tile
        float firstPos = -(_tileGap * Mathf.FloorToInt(_gridSize / 2));
        Vector2 initialPos = new Vector2(firstPos, firstPos);
        GameObject _lastRow = null;

        for (int i = 0; i < _tileCount; i++)
        {
            if (i % _gridSize == 0)
            {
                rowCount++;
                #region Hierarchy organization (Can be deleted)
                _lastRow = new GameObject("Row_" + rowCount);
                _lastRow.transform.parent = _gridObj.transform;
                #endregion
                colCount = 0;
                initialPos.x = firstPos;
                if (i > 0)
                    initialPos.y += _tileGap;
            }

            //_tileList.Add(Instantiate(_gridTilePrefab, _lastRow.transform));
            TileList[colCount, rowCount] = Instantiate(_gridTilePrefab, _lastRow.transform);
            

            // Check if the number of tiles is Odd and then change the position to the center of the GridTile shader
            if (isRowOdd)
            {
                TileList[colCount, rowCount].transform.position = new Vector3(initialPos.x, _tileHeight, initialPos.y);
                TileList[colCount, rowCount].GetComponent<GridTile>().SetTilePos(initialPos.x, _tileHeight, initialPos.y);
            }
            else
            {
                TileList[colCount, rowCount].transform.position = new Vector3(initialPos.x + _tileGap / 2, _tileHeight, initialPos.y + _tileGap / 2);
                TileList[colCount, rowCount].GetComponent<GridTile>().SetTilePos(initialPos.x + _tileGap / 2, _tileHeight, initialPos.y + _tileGap / 2);
            }

            colCount++;
            
            initialPos.x += _tileGap;
        }

        AssignEmptyTiles();
        OnGridReady?.Invoke();
    }

    private void OnValidate()
    {
        _gridMaterial.SetInt("_GridSize", _tilePerRow);
    }
}
