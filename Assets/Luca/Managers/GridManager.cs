using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GridManager : Singleton<GridManager>
{
    [Header("Grid References")]
    [SerializeField]
    private GameObject _gridObj = null;
    [SerializeField]
    private GameObject _gridTilePrefab = null;
    [SerializeField]
    private Material _gridMaterial = null;

    private float _tileGap = 0;
    private int _tileCount = 0;
    private int _gridSize = 0;

    // TO DO Change it to a list of "Tile"
    private List<GameObject> _tileList = new List<GameObject>();

    protected override void Awake()
    {
        base.Awake();
        InitGrid();
    }

    private void InitGrid()
    {
        _tileCount = (int)Mathf.Pow(_gridMaterial.GetInt("_GridSize"), 2);
        _gridSize = _gridMaterial.GetInt("_GridSize");
        _tileGap = _gridObj.transform.localScale.x / _gridMaterial.GetInt("_GridSize");

        bool isRowOdd = _tileCount % 2 != 0;

        // Calc the initialPos of the first tile
        float firstPos = -(_tileGap * Mathf.FloorToInt(_gridMaterial.GetInt("_GridSize") / 2));
        Vector2 initialPos = new Vector2(firstPos, firstPos);
        GameObject _lastRow = null;

        for (int i = 0; i < _tileCount; i++)
        {
            if (i % _gridSize == 0)
            {
                #region Hierarchy organization (Can be deleted)
                _lastRow = new GameObject("Row");
                _lastRow.transform.parent = _gridObj.transform;
                #endregion

                initialPos.x = firstPos;
                if (i > 0)
                    initialPos.y += _tileGap;
            }

            _tileList.Add(Instantiate(_gridTilePrefab, _lastRow.transform));

            // Check if the number of tiles is Odd and then change the position to the center of the GridTile shader
            if (isRowOdd)
                _tileList[i].transform.position = new Vector3(initialPos.x, 0.5f, initialPos.y);
            else
                _tileList[i].transform.position = new Vector3(initialPos.x + _tileGap / 2, 0.5f, initialPos.y + _tileGap / 2);

            initialPos.x += _tileGap;
        }
    }
}
