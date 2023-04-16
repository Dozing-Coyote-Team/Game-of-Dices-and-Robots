using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceManager : Singleton<DiceManager>
{
    //-------------------------- private vars
    [Header("References")]
    [SerializeField] private List<UIDieModel> _uiDice;

    private List<DataDie> _dataDice;
    
    //-------------------------- public methods

    public void RollAll()
    {
        for(int i=0;i<_dataDice.Count;i++)
            RollDie(i);
    }
    
    public void RollDie(int id)
    {
        _dataDice[id].Roll();
    }
    
    public int? GetResult(int id)
    {
        return _dataDice[id].IsRolling ? null : _dataDice[id].Result;
    }
    
    public List<int?> GetResults()
    {
        List<int?> results = new List<int?>();
        foreach (DataDie die in _dataDice)
            results.Add(die.Result);

        return results;
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
        base.Awake();
        _dataDice = new List<DataDie>();
        for(int i=0;i<_uiDice.Count;i++)
            _dataDice.Add(new DataDie(_uiDice[i]));
    }
}
