using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UILootPanel : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private UILootCard _lootCardPrefab;

    public void AddCard(Equipment equipment)
    {
        GameObject cardObj = Instantiate(_lootCardPrefab.gameObject, transform);
        cardObj.GetComponent<UILootCard>().SetEquipment(equipment);
    } 
}
