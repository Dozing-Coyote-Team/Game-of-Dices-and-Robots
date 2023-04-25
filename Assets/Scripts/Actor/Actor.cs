using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    protected List<Equipment> p_equipmentSlots;

    public void ActiveEquipmentsAbility(List<int> dicesResults)
    {
        foreach (Equipment equipment in p_equipmentSlots)
        {
            if (equipment == null)
                continue;

            equipment.ActivateEffect(dicesResults);
        }
    }

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }
}
