using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Enemy : ActorMovement
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
        if (_moveOnIdle)
            MoveRandom();

        // Enemy logic goes here 
        if(GridManager.Instance.CheckInArea(p_currentIndex, _viewRadius, typeof(PlayerMovement), out List<Vector2Int> indexList))
        {
            MoveRandom();
            Debug.Log(indexList[0]);
        }
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
}
