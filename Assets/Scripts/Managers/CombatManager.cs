using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CombatManager : Singleton<CombatManager>
{
    public static event Action OnCombatEnter;
    public static event Action OnCombatExit;

    private Queue<Enemy> _nearEnemies = new Queue<Enemy>();

    public void RegisterEnemy(Enemy enemy)
    {
        _nearEnemies.Enqueue(enemy);
        Debug.Log("Enemy Registered, Name: " + enemy.gameObject);
    }

    public void ClearQueue() => _nearEnemies.Clear();

    // This method is called when a Player find one or more enemy
    public void StartBattle()
    {
        OnCombatEnter?.Invoke();

        // Here the enemies queue is never empty
        // Insert here what to do when the combat start 
        // E.G. start combat with the first dequeued enemy
        CameraManager.Instance.ActiveCombatCamera(_nearEnemies.Peek().gameObject);
        StartCoroutine(CorSetupBattle());
        // Remember to activate the playerCamera after the last battle
    }

    private IEnumerator CorSetupBattle()
    {
        yield return new WaitForSeconds(2f);
        UIManager.Instance.EnableCombatPanel(true);
        DiceManager.Instance.RollAll();
    }

}
