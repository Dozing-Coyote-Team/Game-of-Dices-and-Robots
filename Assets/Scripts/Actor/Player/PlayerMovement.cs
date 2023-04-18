using System;
using UnityEngine;

public class PlayerMovement : ActorMovement
{
    public static event Action OnPlayerMove;

    [Header("Player References")]
    [SerializeField]
    private PlayerCombat _playerCombat;

    protected override void OnEnable()
    {
        base.OnEnable();
        OnMoveStart += InvokeMoveEvent;
        OnMoveStart += CheckEnemies;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        OnMoveStart -= InvokeMoveEvent;
        OnMoveStart -= CheckEnemies;
    }

    private void InvokeMoveEvent() => OnPlayerMove?.Invoke();

    private void CheckEnemies() => _playerCombat.CheckEnemies(p_currentIndex);

    private void Update()
    {
        if (!p_canMove)
            return;

        if (Input.GetKey(KeyCode.W))
        {
            MoveUp();
        }
        else if (Input.GetKey(KeyCode.A))
        {
            MoveLeft();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            MoveDown();
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            MoveRight();
        }
    }
}
