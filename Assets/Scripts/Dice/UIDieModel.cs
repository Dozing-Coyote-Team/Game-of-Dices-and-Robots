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
        StartCoroutine(Rotate(360 * _numRolls));
    }

    public Action OnRollAnimEnd;
    //--------------------------- private methods
    
    // vector.up + vector.right
    //front = 360
    //back = front + 180
    
    private IEnumerator Rotate(float totalRot)
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
            
            transform.RotateAround(transform.position,Vector3.up + Vector3.right,frameRot);

            previousRot = currentRot;
            timeElapsed += Time.deltaTime;
            
            yield return null;
        }
    }

}
