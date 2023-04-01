using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Actor
{
    [Header("Settings")]
    [SerializeField]
    private List<Equipment> _equipmentSlots;

    private void ActiveEquipmentsAbility(List<int> dicesResults)
    {
        foreach(Equipment equipment in _equipmentSlots)
        {
            if(equipment == null)
                continue;

            equipment.ActivateEffect(dicesResults);
        }
    }
}
