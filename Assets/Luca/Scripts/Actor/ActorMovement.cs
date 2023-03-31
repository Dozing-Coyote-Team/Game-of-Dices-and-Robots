using System;
using System.Collections;
using UnityEngine;

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

    protected virtual void Start()
    {
        _currentIndex = _startingIndex;
        transform.position = GridManager.Instance.GetTileAt(_currentIndex).TilePosition;
        GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(false);
    }

    protected virtual void MoveLeft()
    {
        GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(true);
        _currentIndex.x--;

        if(!TryMove(_currentIndex, Vector3.left))
        {
            _currentIndex.x++;
            GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(false);
        }
    }

    protected virtual void MoveRight()
    {
        GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(true);
        _currentIndex.x++;

        if (!TryMove(_currentIndex, Vector3.right))
        {
            _currentIndex.x--;
            GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(false);
        }
    }

    protected virtual void MoveUp()
    {
        GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(true);
        _currentIndex.y++;

        if (!TryMove(_currentIndex, Vector3.forward))
        {
            _currentIndex.y--;
            GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(false);
        }
    }

    protected virtual void MoveDown()
    {
        GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(true);
        _currentIndex.y--;

        if (!TryMove(_currentIndex, -Vector3.forward))
        {
            _currentIndex.y++;
            GridManager.Instance.GetTileAt(_currentIndex).SetEmpty(false);
        }
    }

    private bool TryMove(Vector2Int index, Vector3 direction)
    {
        if (GridManager.Instance.ExistTileAt(index) && GridManager.Instance.GetTileAt(index).IsEmpty)
        {
            StartCoroutine(Move(GridManager.Instance.GetTileAt(index).TilePosition, direction));
            GridManager.Instance.GetTileAt(index).SetEmpty(false);
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
