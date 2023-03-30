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
    private Vector3 _lastRotation = Vector3.forward;

    protected virtual void Start()
    {
        _currentIndex = _startingIndex;
        transform.position = GridManager.Instance.GetTileAt(_currentIndex).TilePosition;
    }

    protected virtual void MoveLeft()
    {
        _currentIndex.x--;
        if (GridManager.Instance.ExistTileAt(_currentIndex) && GridManager.Instance.GetTileAt(_currentIndex).IsEmpty)
            StartCoroutine(Move(GridManager.Instance.GetTileAt(_currentIndex).TilePosition, -Vector3.right));
        else
            _currentIndex.x++;
        
    }

    protected virtual void MoveRight()
    {
        _currentIndex.x++;
        if (GridManager.Instance.ExistTileAt(_currentIndex) && GridManager.Instance.GetTileAt(_currentIndex).IsEmpty)
            StartCoroutine(Move(GridManager.Instance.GetTileAt(_currentIndex).TilePosition, Vector3.right));
        else
            _currentIndex.x--;
    }

    protected virtual void MoveUp()
    {
        _currentIndex.y++;
        if (GridManager.Instance.ExistTileAt(_currentIndex) && GridManager.Instance.GetTileAt(_currentIndex).IsEmpty)
            StartCoroutine(Move(GridManager.Instance.GetTileAt(_currentIndex).TilePosition, Vector3.forward));
        else
            _currentIndex.y--;
    }

    protected virtual void MoveDown()
    {
        _currentIndex.y--;
        if (GridManager.Instance.ExistTileAt(_currentIndex) && GridManager.Instance.GetTileAt(_currentIndex).IsEmpty)
            StartCoroutine(Move(GridManager.Instance.GetTileAt(_currentIndex).TilePosition, -Vector3.forward));
        else
            _currentIndex.y++;
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
