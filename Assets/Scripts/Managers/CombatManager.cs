using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<CombatManager>
{
    public static event Action OnCombatEnter;
    public static event Action OnCombatExit;

    private Queue<EnemyMovement> _enemiesQueue = new Queue<EnemyMovement>();

    public void RegisterEnemy(EnemyMovement enemy)
    {
        _enemiesQueue.Enqueue(enemy);
        Debug.Log("Enemy Registered, Name: " + enemy.gameObject);
    }

    public void ClearQueue() => _enemiesQueue.Clear();

    // This method is called when a Player find one or more enemy
    public void StartBattle()
    {
        OnCombatEnter?.Invoke();

        // Here the enemies queue is never empty
        // Insert here what to do when the combat start 
        // E.G. start combat with the first dequeued enemy
        CameraManager.Instance.ActiveCombatCamera(_enemiesQueue.Peek().gameObject);
        StartCoroutine(CorStartBattle());
        // Remember to activate the playerCamera after the last battle
    }

    private IEnumerator CorStartBattle()
    {
        yield return new WaitForSeconds(1.8f);
        UIManager.Instance.ShowBattlePanel(true);
        yield return new WaitForSeconds(0.2f);
        DiceManagers.Instance.PlayerDM.RollAll();
        DiceManagers.Instance.EnemyDM.RollAll();
    }
}
