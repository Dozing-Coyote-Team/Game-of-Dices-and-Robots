using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Slider EnemyHealthBar => _enemyHealthBar;
    
    [SerializeField] private GameObject _battlePanel;
    [SerializeField] private Slider _enemyHealthBar;

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
