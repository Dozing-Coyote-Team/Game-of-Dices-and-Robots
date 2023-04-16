using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIStatEntry : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private Image _iconImage;
    [SerializeField]
    private TextMeshProUGUI _statText;

    public void SetEntry(Sprite icon, string text)
    {
        _iconImage.sprite = icon;
        _statText.text = text; 
    }
}
