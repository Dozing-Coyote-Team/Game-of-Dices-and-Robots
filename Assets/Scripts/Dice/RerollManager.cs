using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RerollManager : Singleton<RerollManager>
{
    //------------------------- public vars
    public Action OnReroll;
    
    //------------------------- private vars
    private const int NDICE = 5;
    private const int N_REROLLS = 2;
    
    private bool[] _flags;
    private int _availableRerolls;

    private void Awake()
    {
        ResterRerolls();
        _flags = new bool[NDICE];
        for (int i = 0; i < NDICE; i++) 
            _flags[i] = false;
    }

    //------------------------- public methods
    /// <summary>
    /// Flags dice as to roll if not flagged, sets it as not to roll if flagged
    /// </summary>
    /// <param name="id"></param>
    public bool SwitchFlag(int id)
    {
        if (_availableRerolls > 0)
        {
            if (id > _flags.Length)
                Debug.LogError("FlagToRoll called with too high id: " + id);

            _flags[id] = !_flags[id];
            return _flags[id];
        }
        else
            return false;
    }

    public void Reroll()
    {
        if (_availableRerolls > 0)
        {
            for(int i=0;i<NDICE;i++)
                if (_flags[i])
                {                
                    RefManager.Instance.PlayerDM.RollDie(i);
                    _flags[i] = false;
                }

            if (OnReroll!=null)
                OnReroll();

            _availableRerolls--;
            Debug.Log("Available rerolls: "+_availableRerolls);
        }
    }

    public void ResterRerolls()
    {
        _availableRerolls = N_REROLLS;
    }
}
