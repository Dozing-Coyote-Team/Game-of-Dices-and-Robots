using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIDieModel : MonoBehaviour
{
    //--------------------------- private vars
    [SerializeField] private int _numRolls = 3;
    [SerializeField] private float _animDuration = 3f;
    [SerializeField] private AnimationCurve _animationCurve;

    private int _value = 1;
    //--------------------------- public methods
    public void RollTo(int ris)
    { 
        StartCoroutine(Rotate(360 * _numRolls + 120,Vector3.up + Vector3.right + Vector3.back));
    }

    public Action OnRollAnimEnd;
    //--------------------------- private methods
    
    // USATE
    // ================================================== front e back
    // front => StartCoroutine(Rotate(360 * _numRolls, Vector3.up + Vector3.right));
    // back => StartCoroutine(Rotate(360 * _numRolls + 180, Vector3.up + Vector3.right));
    // +- non hanno effetti
    
    // ================================================== left e bottom 
    // bottom => StartCoroutine(Rotate(360 * _numRolls + 120,Vector3.up + Vector3.right + Vector3.forward));
    // bottom => StartCoroutine(Rotate(360 * _numRolls - 120,Vector3.up + Vector3.right + Vector3.forward));
    
    // ================================================== right e top
    //right => StartCoroutine(Rotate(360 * _numRolls + 120,Vector3.up + Vector3.right + Vector3.back));
    //top => StartCoroutine(Rotate(360 * _numRolls - 120,Vector3.up + Vector3.right + Vector3.back));
    
    
    //NON USATE
    // ================================================== right e bottom
    // right => StartCoroutine(Rotate(360 * _numRolls + 120,Vector3.up + Vector3.left + Vector3.forward));
    // bottom => StartCoroutine(Rotate(360 * _numRolls - 120,Vector3.up + Vector3.left + Vector3.forward));
    // front => StartCoroutine(Rotate(360 * _numRolls,Vector3.up + Vector3.left + Vector3.forward));
    // + e - switchano bottom e right
    
    //==================================================  left e top
    // top => StartCoroutine(Rotate(360 * _numRolls + 120,Vector3.up + Vector3.left + Vector3.back));
    // left => StartCoroutine(Rotate(360 * _numRolls - 120,Vector3.up + Vector3.left + Vector3.back));
    
    
    
    private IEnumerator Rotate(float totalRot,Vector3 aroundAxis)
    {
        float t = 0;
        float currentRot = 0;
        float previousRot = 0;

        float frameRot;

        float timeElapsed = 0;

        while (timeElapsed < _animDuration)
        {
            t = timeElapsed / _animDuration;
            t = _animationCurve.Evaluate(t);
            
            currentRot = Mathf.Lerp(0, totalRot, t);

            frameRot = currentRot - previousRot;
            
            Debug.Log("frame rot: "+frameRot);
            
            transform.RotateAround(transform.position,aroundAxis,frameRot);

            previousRot = currentRot;
            timeElapsed += Time.deltaTime;
            
            yield return null;
        }
    }

}
