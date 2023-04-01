using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIDie : MonoBehaviour
{
    //--------------------------- private vars
    [Header("Settings")]
    [SerializeField] private int _dieId;

    private Animator _animator;
    private Text _resultText;
    private string _tempText;   //todo delete
    
    //--------------------------- public methods
    public void RollTo(int ris)
    {
        _tempText = ris.ToString();
        _animator.SetTrigger("Roll");
        _resultText.text = "";
    }

    public Action OnRollAnimEnd;
    //--------------------------- private methods
    private void Awake()
    {
        _resultText = transform.GetChild(0).GetComponent<Text>();
        _animator = GetComponent<Animator>();
        GetComponent<Button>().onClick.AddListener(() => DiceManager.Instance.RollDie(_dieId));
        OnRollAnimEnd += () => _resultText.text = _tempText;
    }

    private void AnimatorCalled_RollAnimEnd() { OnRollAnimEnd(); }
}
