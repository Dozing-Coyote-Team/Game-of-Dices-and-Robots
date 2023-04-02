using System;
using UnityEngine;

public class PlayerMovement : ActorMovement
{
    public static event Action OnPlayerMove;

    protected override void OnEnable()
    {
        base.OnEnable();
        OnMovePerformed += InvokeMoveEvent;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnMovePerformed -= InvokeMoveEvent;
    }

    private void InvokeMoveEvent() => OnPlayerMove?.Invoke();

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
