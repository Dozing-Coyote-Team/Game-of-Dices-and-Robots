using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Actor : MonoBehaviour
{
    //------------------------ public vars
    public int Healht { get; private set; }

    //------------------------ private vars
    [Header("Settings")]
    [SerializeField]
    protected List<Equipment> p_equipmentSlots;
    [SerializeField] protected int _maxHealth;
    
    private List<Slider> _healthBars;

    public void ActiveEquipmentsAbility(List<int> dicesResults)
    {
        foreach (Equipment equipment in p_equipmentSlots)
        {
            if (equipment == null)
                continue;

            equipment.ActivateEffect(dicesResults);
        }
    }

    public void AttachHealthBar(Slider bar)
    {
        bar.maxValue = _maxHealth;
        bar.value = Healht;
        _healthBars.Add(bar);
    }
    
    public void TakeDamage(int damage)
    {
        Healht -= damage;

        if (Healht <= 0)
        {
            Healht = 0;
            Destroy(gameObject);
        }

        foreach (Slider hpBar in  _healthBars)
        {
            hpBar.value = Healht;
        }
    }

    private void Awake()
    {
        _healthBars = new List<Slider>();
        Healht = _maxHealth;
    }
}
