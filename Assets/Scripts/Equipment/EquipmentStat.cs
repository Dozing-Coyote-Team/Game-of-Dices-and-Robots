using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CreateAssetMenu(fileName = "EquipStat", menuName = "Equip Stat")]
public class EquipmentStat : ScriptableObject
{
    public enum EquipmentType
    {
        Head,
        Torso,
        Arms,
        Legs
    }

    public enum ComboType
    {
        Kind3,
        Kind4,
        FullHouse,
        LowStraight,
        HighStraight,
        Kind5,
        CustomCombo
    }
    
    public EquipmentType EquipType { get => _equipType; }
    public ComboType Combo { get => _comboType; }
    public int Damage { get => _damage; }
    public int Defence { get => _defence; }
    public int Accuracy { get => _accuracy; }
    public int Speed { get => _speed; }

    [Header("Statistics")]
    [SerializeField]
    private EquipmentType _equipType;
    [SerializeField]
    private ComboType _comboType;
    [Min(0)]
    [SerializeField]
    private int _damage;
    [Min(0)]
    [SerializeField]
    private int _defence;
    [Min(0)]
    [SerializeField]
    private int _accuracy;
    [Min(0)]
    [SerializeField]
    private int _speed;
    [HideInInspector]
    [SerializeField]
    private List<int> _customComboSet = new List<int>(5);

    public bool IsComboValid(List<int> dicesResults)
    {
        // Combo Kind
        if((_comboType == ComboType.Kind3 || _comboType == ComboType.Kind4 || _comboType == ComboType.Kind5))
        {
            int countEqualNumbers = dicesResults.GroupBy(n => n)
                      .Select(g => g.Count())
                      .DefaultIfEmpty(0)
                      .Max();
            if (countEqualNumbers == 3 && _comboType == ComboType.Kind3)
                return true;
            if (countEqualNumbers == 4 && _comboType == ComboType.Kind4)
                return true;
            if (countEqualNumbers == 5 && _comboType == ComboType.Kind5)
                return true;
        }

        // Full House
        if(_comboType == ComboType.FullHouse)
        {
           return dicesResults.GroupBy(n => n)
                             .Where(g => g.Count() == 2 || g.Count() == 3)
                             .GroupBy(g => g.Count())
                             .Select(g => g.Key)
                             .SequenceEqual(new[] { 2, 3 });
        }

        // High Straight
        if (_comboType == ComboType.HighStraight)
        {
            return dicesResults.OrderBy(n => n)
                                    .Select((n, i) => new { Number = n, Index = i })
                                    .GroupBy(x => x.Number - x.Index)
                                    .Any(g => g.Count() == 5);
        }

        // Low Straight
        if (_comboType == ComboType.LowStraight)
        {
            return dicesResults.OrderBy(n => n)
                                .Select((n, i) => new { Number = n, Index = i })
                                .GroupBy(x => x.Number - x.Index)
                                .Any(g => g.Count() == 4);
        }

        // Custom Combo
        if(_comboType == ComboType.CustomCombo)
        {
            for(int i=0; i < dicesResults.Count; i++)
            {
                if ((_customComboSet[i] != 0) && (_customComboSet[i] != dicesResults[i]))
                    return false;
            }
            return true;
        }

        return false;
    }
}
