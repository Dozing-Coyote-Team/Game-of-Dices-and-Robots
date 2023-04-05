using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EquipmentStat))]
public class EquipmentStatCustomEdItor : Editor
{
    private SerializedProperty _comboType;
    private SerializedProperty _customComboSet;

    private void OnEnable()
    {
        _comboType = serializedObject.FindProperty("_comboType");
        _customComboSet = serializedObject.FindProperty("_customComboSet");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();

        EquipmentStat.ComboType combo = (EquipmentStat.ComboType)_comboType.intValue;

        if(combo == EquipmentStat.ComboType.CustomCombo)
        {
            _customComboSet.arraySize = 5;
            EditorGUILayout.PropertyField(_customComboSet);

            for(int i = 0; i < _customComboSet.arraySize; i++)
            {
                if(_customComboSet.GetArrayElementAtIndex(i).intValue < 0 || _customComboSet.GetArrayElementAtIndex(i).intValue > 6)
                {
                    _customComboSet.GetArrayElementAtIndex(i).intValue = 0;
                }
            }

            serializedObject.ApplyModifiedProperties();       
        }
    }
}
