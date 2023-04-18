using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    [SerializeField] private GameObject _battlePanel;

    protected override void Awake()
    {
        base.Awake();
        ShowBattlePanel(false);
    }

    public void ShowBattlePanel(bool show)
    {
        _battlePanel.SetActive(show);
    }
}
