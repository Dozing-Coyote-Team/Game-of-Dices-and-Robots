using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefManager : Singleton<RefManager>
{
    [SerializeField] private Player _player;
    [SerializeField] private DiceManager _playerDm;
    [SerializeField] private DiceManager _enemyDm;

    public Player Player => _player;
    public DiceManager PlayerDM => _playerDm;
    public DiceManager EnemyDM => _enemyDm;
}
