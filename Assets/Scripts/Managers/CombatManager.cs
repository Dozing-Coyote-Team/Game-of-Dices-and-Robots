using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<CombatManager>
{
    public static event Action OnCombatReady;
    public static event Action OnCombatStart;

    private Queue<Enemy> _enemiesQueue = new Queue<Enemy>();

    public void RegisterEnemy(Enemy enemy)
    {
        _enemiesQueue.Enqueue(enemy);
        Debug.Log("Enemy Register, Name: " + enemy.gameObject);
    } 
    

}
