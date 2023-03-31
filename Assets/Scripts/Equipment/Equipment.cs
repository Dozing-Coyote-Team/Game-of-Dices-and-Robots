using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Inherits from this class for implementing different effects
public abstract class Equipment : MonoBehaviour
{
    public EquipmentStat Stat { get => p_stat; }

    [Header("References")]
    [SerializeField]
    protected EquipmentStat p_stat;

    // Call in order to activate the equipment's effect,
    // if the combo requirement is met.
    public void ActivateEffect(List<int> dicesResults)
    {
        if (p_stat.IsComboValid(dicesResults))
            ApplyEffect();
    }

    protected abstract void ApplyEffect();
}
