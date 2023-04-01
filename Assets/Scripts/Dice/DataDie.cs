using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataDie
{
    //-------------------------- public vars
    /// <summary>
    /// null if dice rolling, [1,6] otherwise
    /// </summary>
    public int? Result { get => IsRolling ? null : _result; }
    public bool IsRolling { get; private set; } = false;
    
    //-------------------------- private vars
    private int _result = 1;

    private UIDie _uiDie;
    //-------------------------- public methods
    public DataDie(UIDie uiDie)
    {
        _uiDie = uiDie;
        _uiDie.OnRollAnimEnd += () => IsRolling = false;
    }
    
    public void Roll()
    {
        IsRolling = true;
        _result = Random.Range(1, 7);
        _uiDie.RollTo(_result);
    }
}
