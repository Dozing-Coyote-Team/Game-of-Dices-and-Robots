using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIDieModel : MonoBehaviour
{
    //--------------------------- private vars
    private Animator _animator;

    //--------------------------- public methods
    public void RollTo(int ris)
    {
        _animator.SetTrigger("Roll");
    }

    public Action OnRollAnimEnd;
    //--------------------------- private methods
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void AnimatorCalled_RollAnimEnd() { OnRollAnimEnd(); }
}
