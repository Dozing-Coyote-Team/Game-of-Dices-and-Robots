using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : MonoBehaviour
{
    //-------------------------- public vars
    public int TotalAttack =>  GetResult(eDiceTypes.Attack1) + GetResult(eDiceTypes.Attack2);
    public int TotalDefense => GetResult(eDiceTypes.Defense1) + GetResult(eDiceTypes.Defense2);
    
    public enum eDiceTypes
    {
        Attack1,Attack2,Defense1,Defense2,Speed
    }
    //-------------------------- private vars
    [Header("References")]
    [SerializeField] private List<UIDieModel> _uiDice;
    private List<DataDie> _dataDice;

    //-------------------------- public methods
    public void Roll(eDiceTypes dice)
    {
        _dataDice[(int)dice].Roll();
    }

    public void RollAll()
    {
        for (int i=0;i<_uiDice.Count;i++)
            _dataDice[i].Roll();
    }
    
    public int GetResult(eDiceTypes dice)
    {
        if(_dataDice[(int)dice].IsRolling)
            Debug.LogError("Get dice result called while dice rolling");
        
        return (int)_dataDice[(int)dice].Result;
    }
    
    public bool IsAnyDiceRolling()
    {
        foreach(DataDie die in _dataDice)
            if (die.IsRolling)
                return true;
        return false;
    }

    //-------------------------- private methods
    private void Awake()
    {
        _dataDice = new List<DataDie>();
        for(int i=0;i<_uiDice.Count;i++)
            _dataDice.Add(new DataDie(_uiDice[i]));
    }
    
}
