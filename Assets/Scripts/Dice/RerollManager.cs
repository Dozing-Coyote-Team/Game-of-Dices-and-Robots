using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollManager : Singleton<RerollManager>
{

    private const int NDICE = 5;
    private bool[] _flags;

    private void Awake()
    {
        _flags = new bool[NDICE];
        for (int i = 0; i < NDICE; i++) 
            _flags[i] = false;
    }

    /// <summary>
    /// Flags dice as to roll if not flagged, sets it as not to roll if flagged
    /// </summary>
    /// <param name="id"></param>
    public bool SwitchFlag(int id)
    {
        if (id > _flags.Length)
            Debug.LogError("FlagToRoll called with too high id: "+id);

        _flags[id] = !_flags[id];
        return _flags[id];
    }

    public void Reroll()
    {
        for(int i=0;i<NDICE;i++)
            if (_flags[i])
            {                
                DiceManagers.Instance.PlayerDM.RollDie(i);
                _flags[i] = false;
            }

        if (OnReroll!=null)
            OnReroll();
    }

    public Action OnReroll;
}
