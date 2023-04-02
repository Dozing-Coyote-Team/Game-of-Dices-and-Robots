using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ActorMovement : MonoBehaviour
{
    protected bool p_canMove { get; private set; } = true;

    [Header("Settings")]
    [Min(0)]
    [SerializeField]
    private float _moveTime = 0.5f;
    [Min(0)]
    [SerializeField]
    private Vector2Int _startingIndex = Vector2Int.zero;

    private Vector2Int _currentIndex = Vector2Int.zero;

    protected virtual void OnEnable()
    {
        GridManager.OnGridReady += InstantiateActor;
    }

    protected virtual void OnDisable()
    {
        GridManager.OnGridReady -= InstantiateActor;
    }

    protected virtual void InstantiateActor()
    {
        _currentIndex = _startingIndex;
        if (GridManager.Instance.ExistTileAt(_currentIndex) && GridManager.Instance.GetTileAt(_currentIndex).IsEmpty)
            transform.position = GridManager.Instance.GetTileAt(_currentIndex).TilePosition;
        else
        {
            _currentIndex = GetFirstEmpty(Vector2Int.zero);
            transform.position = GridManager.Instance.GetTileAt(_currentIndex).TilePosition;
        }

        GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(false, this.gameObject);
    }

    protected virtual Vector2Int GetFirstEmpty(Vector2Int startIndex)
    {  
        for(int x = startIndex.x; x < GridManager.Instance.TileList.GetLength(0); x++)
        {
            for (int y = startIndex.y; y < GridManager.Instance.TileList.GetLength(1); y++)
            {
                if (GridManager.Instance.GetTileAt(new Vector2Int(x, y)).IsEmpty)
                    return new Vector2Int(x, y);
            }
        }

        return new Vector2Int(-1, -1);
    } 

    protected virtual void MoveLeft()
    {
        GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(true, this.gameObject);
        _currentIndex.x--;

        if(!TryMove(_currentIndex, Vector3.left))
        {
            _currentIndex.x++;
            GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(false, this.gameObject);
        }
    }

    protected virtual void MoveRight()
    {
        GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(true, this.gameObject);
        _currentIndex.x++;

        if (!TryMove(_currentIndex, Vector3.right))
        {
            _currentIndex.x--;
            GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(false, this.gameObject);
        }
    }

    protected virtual void MoveUp()
    {
        GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(true, this.gameObject);
        _currentIndex.y++;

        if (!TryMove(_currentIndex, Vector3.forward))
        {
            _currentIndex.y--;
            GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(false, this.gameObject);
        }
    }

    protected virtual void MoveDown()
    {
        GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(true, this.gameObject);
        _currentIndex.y--;

        if (!TryMove(_currentIndex, -Vector3.forward))
        {
            _currentIndex.y++;
            GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(false, this.gameObject);
        }
    }

    private bool TryMove(Vector2Int index, Vector3 direction)
    {
        if (GridManager.Instance.ExistTileAt(index) && GridManager.Instance.GetTileAt(index).IsEmpty)
        {
            StartCoroutine(Move(GridManager.Instance.GetTileAt(index).TilePosition, direction));
            GridManager.Instance.GetTileAt(index).SetEmpty(false, this.gameObject);

            //Dictionary<Vector2Int, GameObject> dic = GridManager.Instance.GetNeighboursObj(index);

            //foreach(var obj in dic)
            //{
            //    Debug.Log(obj.Value.ToString());
            //}

            return true;
        }

        return false;
    }

    private IEnumerator Move(Vector3 position, Vector3 rotation)
    {
        p_canMove = false;
        float elapsedTime = 0;
        float waitTime = _moveTime;
        Vector3 currentPos = transform.position;

        transform.rotation = Quaternion.LookRotation(rotation);

        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPos, position, elapsedTime / waitTime);
            yield return null;
        }

        

        p_canMove = true;
    }
}
