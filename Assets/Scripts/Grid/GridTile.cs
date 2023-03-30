using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public Vector3 TilePosition { get; private set; } = Vector3.zero;
    public bool IsEmpty { get; private set; } = true;

    public void SetTilePos(float x, float y, float z)
    {
        TilePosition = new Vector3(x, y, z);
    }

    public void SetEmpty(bool isEmpty)
    {
        IsEmpty = isEmpty;
    }

}
