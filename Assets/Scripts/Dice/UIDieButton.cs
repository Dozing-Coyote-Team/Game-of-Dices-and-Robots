using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIDieButton : MonoBehaviour
{
    [FormerlySerializedAs("id")]
    [Header("Settings")]
    [SerializeField] private int _id = 0;

    [Header("References")] 
    [SerializeField] private GameObject _flaggedIndicator;

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(()=>WhenTapped());
        _flaggedIndicator.SetActive(false);
        RerollManager.Instance.OnReroll += () => _flaggedIndicator.SetActive(false);
    }

    private void WhenTapped()
    {
        _flaggedIndicator.SetActive(RerollManager.Instance.SwitchFlag(_id));
    }
}
