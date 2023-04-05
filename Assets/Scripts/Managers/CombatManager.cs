using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<CombatManager>
{
    public static event Action OnCombatStart;

    private Queue<Enemy> _enemiesQueue = new Queue<Enemy>();

    public void RegisterEnemy(Enemy enemy)
    {
        _enemiesQueue.Enqueue(enemy);
        Debug.Log("Enemy Registered, Name: " + enemy.gameObject);
    }

    public void ClearQueue() => _enemiesQueue.Clear();

    // This method is called when a Player find one or more enemy
    public void StartCombat()
    {
        OnCombatStart?.Invoke();
        // Insert here what to do when the combat start 
        // E.G. start combat with the first dequeued enemy
    }
}
