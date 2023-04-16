using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDieButton : MonoBehaviour
{
    [SerializeField] private int id = 0;

    private void Awake()
    {
        Debug.Log("Awake called");
        GetComponent<Button>().onClick.AddListener(() => Roll());
    }

    private void Roll()
    {
        Debug.Log("Rolling die");
        DiceMngGetter.Instance.PlayerDiceManager().RollDie(id);
    }
}
