using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTile : MonoBehaviour
{
    public Vector3 TilePosition { get; private set; } = Vector3.zero;
    public bool IsEmpty { get; private set; } = true;
    public GameObject TileObject { get; private set; } = null;

    public void SetTilePos(float x, float y, float z)
    {
        TilePosition = new Vector3(x, y, z);
    }

    public void SetEmpty(bool isEmpty, GameObject currObject)
    {
        IsEmpty = isEmpty;

        if(IsEmpty)
            TileObject = null;
        else
            TileObject = currObject;
    }

    public void AutoSetEmpty()
    {
        RaycastHit hit;
        Vector3 fwd = transform.TransformDirection(Vector3.up);

        if (Physics.Raycast(transform.position, fwd, out hit, 10))
        {
            IsEmpty = false;
            TileObject = hit.collider.gameObject;
        }
        else
            IsEmpty = true;
    }
}
