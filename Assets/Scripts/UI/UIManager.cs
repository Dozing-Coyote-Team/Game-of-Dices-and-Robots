using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _combatPanel;

    protected override void Awake()
    {
        base.Awake();
        EnableCombatPanel(false);
    }

    public void EnableCombatPanel(bool enable)
    {
        _combatPanel.SetActive(enable);
    }
}
