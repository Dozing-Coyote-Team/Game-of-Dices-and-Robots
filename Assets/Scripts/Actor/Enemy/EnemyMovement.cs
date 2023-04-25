using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : ActorMovement
{
    [Header("Movement Settings")]
    [SerializeField]
    private int _viewRadius = 2;
    [SerializeField]
    private bool _moveOnIdle = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        PlayerMovement.OnPlayerMove += TakeDecision;
    }

    protected override void OnDisable()
    {
        base.OnEnable();
        PlayerMovement.OnPlayerMove -= TakeDecision;
    }

    private void TakeDecision()
    {
        if (!CheckForPlayer() && _moveOnIdle)
            MoveRandom();
    }

    private void MoveRandom()
    {
        int[] moveList = { 0, 1, 2, 3};
        moveList = moveList.OrderBy(x => Random.Range(0,4)).ToArray();
        foreach(int move in moveList)
        {
            if (p_moved)
                return;

            switch (move)
            {
                case 0:
                    MoveUp();
                    break;
                case 1:
                    MoveDown();
                    break;
                case 2:
                    MoveLeft();
                    break;
                case 3:
                    MoveRight();
                    break;
            }
        }
    }

    private bool CheckForPlayer()
    {
        if (GridManager.Instance.CheckInArea(p_currentIndex, _viewRadius, typeof(PlayerMovement), out List<Vector2Int> indexList))
        {
            Dictionary<Vector2Int, int> resultDirection = new Dictionary<Vector2Int, int>();
            List<Vector2Int> indexes = new List<Vector2Int>() { new Vector2Int(p_currentIndex.x + 1, p_currentIndex.y),
                                                                new Vector2Int(p_currentIndex.x, p_currentIndex.y + 1),
                                                                new Vector2Int(p_currentIndex.x - 1, p_currentIndex.y),
                                                                new Vector2Int(p_currentIndex.x, p_currentIndex.y - 1),
                                                                new Vector2Int(p_currentIndex.x, p_currentIndex.y)};
            
            for(int i = 0; i < indexes.Count; i++)
            {
                if (GridManager.Instance.GetTileAt(indexes[i]) != null)
                    resultDirection.Add(indexes[i], GridManager.Instance.ManhattanDistance(indexes[i], indexList[0]));
            }

            resultDirection = resultDirection.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);

            for (int i = 0; i < resultDirection.Count; i++)
            {
                if (p_moved)
                    return true;

                Debug.Log(resultDirection.ElementAt(i).Key);

                if (resultDirection.ElementAt(i).Key == indexes[1])
                    MoveUp();
                else if (resultDirection.ElementAt(i).Key == indexes[3])
                    MoveDown();
                else if (resultDirection.ElementAt(i).Key == indexes[2])
                    MoveLeft();
                else if (resultDirection.ElementAt(i).Key == indexes[0])
                    MoveRight();
                else if (resultDirection.ElementAt(i).Key == indexes[4])
                    return true;
            }
        }
        return false;
    }
}
