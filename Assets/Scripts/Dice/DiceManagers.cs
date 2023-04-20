using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManagers : Singleton<DiceManagers>
{
    [SerializeField] private DiceManager _playerDm;
    [SerializeField] private DiceManager _enemyDm;

    public DiceManager PlayerDM => _playerDm;
    public DiceManager EnemyDM => _enemyDm;
}
