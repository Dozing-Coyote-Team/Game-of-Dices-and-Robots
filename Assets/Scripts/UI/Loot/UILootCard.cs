using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILootCard : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private UIStatEntry _statEntryPrefab;
    [SerializeField]
    private GameObject _statPanel;
    [SerializeField]
    private Image _equipIcon;

    [Header("Icons References")]
    [SerializeField]
    private Sprite _damgeIcon;
    [SerializeField]
    private Sprite _defenceIcon;
    [SerializeField]
    private Sprite _speedIcon;
    [SerializeField]
    private Sprite _accuracyIcon;

    public void SetEquipment(Equipment equip)
    {
        EquipmentStat stat = equip.Stat;

        if(stat.Damage > 0)
        {
            AddEntry(_damgeIcon, stat.Damage);
        }
        if (stat.Defence > 0)
        {
            AddEntry(_defenceIcon, stat.Defence);
        }
        if (stat.Speed > 0)
        {
            AddEntry(_speedIcon, stat.Speed);
        }
        if (stat.Accuracy > 0)
        {
            AddEntry(_accuracyIcon, stat.Accuracy);
        }

        _equipIcon.sprite = stat.Icon;
    }

    private void AddEntry(Sprite statIcon, int statValue)
    {
        GameObject entryObj = Instantiate(_statEntryPrefab.gameObject, _statPanel.transform);
        entryObj.GetComponent<UIStatEntry>().SetEntry(statIcon, "+"+statValue.ToString("000"));
    }
}
