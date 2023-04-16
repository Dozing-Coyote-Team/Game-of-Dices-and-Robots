using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceMngGetter : Singleton<DiceMngGetter>
{
    [SerializeField] private DiceManager _playerDiceMng;
    [SerializeField] private DiceManager _enemyDiceMng;

    public DiceManager PlayerDiceManager() => _playerDiceMng;
    public DiceManager EnemyDiceManager() => _enemyDiceMng;

}
