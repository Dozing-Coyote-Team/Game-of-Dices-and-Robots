using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using static DiceManager;
using Random = UnityEngine.Random;

public class CombatManager : Singleton<CombatManager>
{
    public static event Action OnCombatEnter;
    public static event Action OnCombatExit;

    private Player _player;
    private DiceManager _playerDm;
    private DiceManager _enemyDm;
    
    private Queue<EnemyMovement> _enemiesQueue = new Queue<EnemyMovement>();

    private Enemy _currentEnemy;

    private bool _isTurnEnding = false;

    public void RegisterEnemy(EnemyMovement enemy)
    {
        _enemiesQueue.Enqueue(enemy);
        Debug.Log("Enemy Registered, Name: " + enemy.gameObject);
    }

    public void ClearQueue() => _enemiesQueue.Clear();

    private void Start()
    {
        _player = RefManager.Instance.Player;
        _playerDm = RefManager.Instance.PlayerDM;
        _enemyDm = RefManager.Instance.EnemyDM;
    }

    // This method is called when a Player find one or more enemy
    public void StartBattle()
    {
        OnCombatEnter?.Invoke();
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
        
        _playerDm.RollAll();
        _enemyDm.RollAll();
    }

    private void EndBattle()
    {
        RerollManager.Instance.ResterRerolls();
        _isTurnEnding = false;
        UIManager.Instance.ShowBattlePanel(false);
        CameraManager.Instance.ActivePlayerCamera();
    }
    
    public void EndTurn()
    {
        if (!_isTurnEnding)
        {
            _isTurnEnding = true;
            StartCoroutine(CorEndTurn());
        }
    }

    private IEnumerator CorEndTurn()
    {
        while (_playerDm.IsAnyDiceRolling() || _enemyDm.IsAnyDiceRolling())
            yield return new WaitForSeconds(.1f);
        
        yield return StartCoroutine(CorHandleCombat());
        
        _playerDm.RollAll();
        _enemyDm.RollAll();
        RerollManager.Instance.ResterRerolls();
        _isTurnEnding = false;
    }

    private IEnumerator CorHandleCombat()
    {
        if (IsPlayerFaster())
        {
            _currentEnemy.TakeDamage(EvaluateFinalDamage(_playerDm,_enemyDm));
            CheckEnemyHealth();
            
            yield return new WaitForSeconds(1f);
            
            _player.TakeDamage(EvaluateFinalDamage(_enemyDm,_playerDm));
            CheckPlayerHealth();
        }
        else
        {
            _player.TakeDamage(EvaluateFinalDamage(_enemyDm,_playerDm));
            CheckPlayerHealth();
            
            yield return new WaitForSeconds(1f);
            
            _currentEnemy.TakeDamage(EvaluateFinalDamage(_playerDm,_enemyDm));
            CheckEnemyHealth();
        }
        //-------------------------- private methods
        bool IsPlayerFaster()
        {
            int playerSpeed = _playerDm.GetResult(eDiceTypes.Speed);
            int enemySpeed = _enemyDm.GetResult(eDiceTypes.Speed);

            if (playerSpeed > enemySpeed)
            {
                Debug.Log("player is faster");
                return true;
            }
            if (playerSpeed < enemySpeed)
            {
                Debug.Log("player is slower");
                return false;
            }
            Debug.Log("player and enemy have same speed");
            if (Random.Range(0, 2) == 0)
                return true;
            else 
                return false;
            
            
        }

        int EvaluateFinalDamage(DiceManager attacker, DiceManager defender)
        {
            int ris = attacker.TotalAttack - defender.TotalDefense;
            
            if (ris < 0)
                ris = 0;
            
            return ris;
        }

        void CheckPlayerHealth()
        {
            if (_player.Healht == 0)
            {
                GameLoopManager.Instance.GameOver();
                EndBattle();
            }
        }

        void CheckEnemyHealth()
        {
            if (_currentEnemy.Healht == 0)
                EndBattle();
        }
    }

}
