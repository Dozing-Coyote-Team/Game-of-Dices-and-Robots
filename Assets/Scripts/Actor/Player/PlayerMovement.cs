using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : ActorMovement
{
    public static event Action OnPlayerMove;

    protected override void OnEnable()
    {
        base.OnEnable();
        OnMovePerformed += InvokeMoveEvent;
        OnMovePerformed += CheckEnemies;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnMovePerformed -= InvokeMoveEvent;
        OnMovePerformed -= CheckEnemies;
    }

    private void InvokeMoveEvent() => OnPlayerMove?.Invoke();

    private void CheckEnemies()
    {
        List<Enemy> result = GridManager.Instance.GetNeighbourEnemies(p_currentIndex);
        for(int i =  0; i < result.Count; i++)
        {
            CombatManager.Instance.RegisterEnemy(result[i]);
        }
    }

    private void Update()
    {
        if (!p_canMove)
            return;

        if (Input.GetKeyDown(KeyCode.W))
        {
            MoveUp();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            MoveDown();
        }
        else if (Input.GetKeyDown(KeyCode.D)) 
        {
            MoveRight();
        }
    }
}
