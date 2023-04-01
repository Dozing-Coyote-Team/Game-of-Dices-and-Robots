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

    private UIDieModel _uiDieModel;
    //-------------------------- public methods
    public DataDie(UIDieModel uiDieModel)
    {
        _uiDieModel = uiDieModel;
        _uiDieModel.OnRollAnimEnd += () => IsRolling = false;
    }
    
    public void Roll()
    {
        IsRolling = true;
        _result = Random.Range(1, 7);
        _uiDieModel.RollTo(_result);
    }
}
