using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : Singleton<CombatManager>
{
    public static event Action OnCombatEnter;
    public static event Action OnCombatExit;

    [SerializeField] private Player _player;
    
    private Queue<EnemyMovement> _enemiesQueue = new Queue<EnemyMovement>();

    private Enemy _currentEnemy;
    
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
        _currentEnemy = _enemiesQueue.Peek().gameObject.GetComponent<Enemy>();
        StartCoroutine(CorStartBattle());
    }

    private IEnumerator CorStartBattle()
    {
        yield return new WaitForSeconds(1.8f);
        UIManager.Instance.ShowBattlePanel(true);
        if(_currentEnemy == null)Debug.Log("Current enemy null");
        _currentEnemy.AttachHealthBar(UIManager.Instance.EnemyHealthBar);
        
        yield return new WaitForSeconds(0.2f);
        RefManager.Instance.PlayerDM.RollAll();
        RefManager.Instance.EnemyDM.RollAll();
    }

    private void EndBattle()
    {
        UIManager.Instance.ShowBattlePanel(false);
        CameraManager.Instance.ActivePlayerCamera();
    }
    
    public void EndTurn()
    {
        StartCoroutine(EndTurnCor());
    }

    private IEnumerator EndTurnCor()
    {
        while (RefManager.Instance.PlayerDM.IsAnyDiceRolling() || RefManager.Instance.EnemyDM.IsAnyDiceRolling())
            yield return new WaitForSeconds(.1f);


        _player.TakeDamage((int)RefManager.Instance.EnemyDM.GetResult(0));
        _currentEnemy.TakeDamage((int)RefManager.Instance.PlayerDM.GetResult(0));

        if (_player.Healht == 0)
        {
            GameLoopManager.Instance.GameOver();
            yield break;
        }

        if (_currentEnemy.Healht == 0)
        {
            EndBattle();
            yield break;
        }

        RefManager.Instance.PlayerDM.RollAll();
        RefManager.Instance.EnemyDM.RollAll();
        RerollManager.Instance.ResterRerolls();
    }
}
