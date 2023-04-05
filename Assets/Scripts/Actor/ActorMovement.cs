using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ActorMovement : MonoBehaviour
{
    protected event Action OnMovePerformed; 
    protected bool p_canMove { get; private set; } = true;
    protected bool p_moved { get; private set; } = false;

    protected Vector2Int p_currentIndex = Vector2Int.zero;

    [Header("Movement Settings")]
    [Min(0)]
    [SerializeField]
    private float _moveTime = 0.5f;
    [Min(0)]
    [SerializeField]
    private Vector2Int _startingIndex = Vector2Int.zero;


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
        p_currentIndex = _startingIndex;
        if (GridManager.Instance.ExistTileAt(p_currentIndex) && GridManager.Instance.GetTileAt(p_currentIndex).IsEmpty)
            transform.position = GridManager.Instance.GetTileAt(p_currentIndex).TilePosition;
        else
        {
            p_currentIndex = GetFirstEmpty(Vector2Int.zero);
            transform.position = GridManager.Instance.GetTileAt(p_currentIndex).TilePosition;
        }

        GridManager.Instance.GetTileAt(p_currentIndex).SetEmpty(false, this.gameObject);
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
        GridManager.Instance.GetTileAt(p_currentIndex).SetEmpty(true, this.gameObject);
        p_currentIndex.x--;

        if(!TryMove(p_currentIndex, Vector3.left))
        {
            p_currentIndex.x++;
            GridManager.Instance.GetTileAt(p_currentIndex).SetEmpty(false, this.gameObject);
        }
    }

    protected virtual void MoveRight()
    {
        GridManager.Instance.GetTileAt(p_currentIndex).SetEmpty(true, this.gameObject);
        p_currentIndex.x++;

        if (!TryMove(p_currentIndex, Vector3.right))
        {
            p_currentIndex.x--;
            GridManager.Instance.GetTileAt(p_currentIndex).SetEmpty(false, this.gameObject);
        }
    }

    protected virtual void MoveUp()
    {
        GridManager.Instance.GetTileAt(p_currentIndex).SetEmpty(true, this.gameObject);
        p_currentIndex.y++;

        if (!TryMove(p_currentIndex, Vector3.forward))
        {
            p_currentIndex.y--;
            GridManager.Instance.GetTileAt(p_currentIndex).SetEmpty(false, this.gameObject);
        }
    }

    protected virtual void MoveDown()
    {
        GridManager.Instance.GetTileAt(p_currentIndex).SetEmpty(true, this.gameObject);
        p_currentIndex.y--;

        if (!TryMove(p_currentIndex, -Vector3.forward))
        {
            p_currentIndex.y++;
            GridManager.Instance.GetTileAt(p_currentIndex).SetEmpty(false, this.gameObject);
        }
    }

    private bool TryMove(Vector2Int index, Vector3 direction)
    {
        if (GridManager.Instance.ExistTileAt(index) && GridManager.Instance.GetTileAt(index).IsEmpty)
        {
            GridManager.Instance.GetTileAt(index).SetEmpty(false, this.gameObject);
            StartCoroutine(Move(GridManager.Instance.GetTileAt(index).TilePosition, direction));
            p_moved = true;
            return true;
        }
        p_moved = false;
        return false;
    }

    private IEnumerator Move(Vector3 position, Vector3 rotation)
    {
        p_canMove = false;
        float elapsedTime = 0;
        float waitTime = _moveTime;
        Vector3 currentPos = transform.position;
        
        OnMovePerformed?.Invoke();

        transform.rotation = Quaternion.LookRotation(rotation);

        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
            transform.position = Vector3.Lerp(currentPos, position, elapsedTime / waitTime);
            yield return null;
        }

        p_moved = false;
        p_canMove = true;
    }
}
