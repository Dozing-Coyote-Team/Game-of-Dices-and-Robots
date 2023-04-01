using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UIDieModel : MonoBehaviour
{
    //--------------------------- private vars
    [SerializeField] private int _numRolls = 3;
    [SerializeField] private float _animDuration = 3f;
    [SerializeField] private AnimationCurve _animationCurve;
    [SerializeField] private List<UIDieModelRotation> _frontRotations;
    [SerializeField] private UIDieModelRotation _backRotation;
    [SerializeField] private UIDieModelRotation _rightRotation;
    [SerializeField] private UIDieModelRotation _leftRotaion;
    [SerializeField] private UIDieModelRotation _upRotation;
    [SerializeField] private UIDieModelRotation _bottomRotation;
    
    private int _value = 1;
    //--------------------------- public methods
    public void RollTo(int ris)
    {
        UIDieModelRotation rotation = new UIDieModelRotation();
        // if (ris == _value)
        // {
        //     rotation = _frontRotations[Random.Range(0,_frontRotations.Count)];
        // }
        // else if()

        int rand = Random.Range(0, 2);
        switch (rand)
        {
            case 0:
                rotation = _backRotation;
                break;
            case 1:
                rotation = _rightRotation;
                break;
            case 2:
                rotation = _leftRotaion;
                break;
            case 3:
                rotation = _upRotation;
                break;
            case 4:
                rotation = _bottomRotation;
                break;
        }
        
        
        
        StartCoroutine(Rotate(360 * _numRolls + rotation.RotationOffset,rotation.Axis));
    }

    public Action OnRollAnimEnd;
    //--------------------------- private methods
    
    // USATE
    // ================================================== left e bottom 
    // bottom => StartCoroutine(Rotate(360 * _numRolls + 120,Vector3.up + Vector3.right + Vector3.forward));
    // bottom => StartCoroutine(Rotate(360 * _numRolls - 120,Vector3.up + Vector3.right + Vector3.forward));
    
    // ================================================== right e top
    //right => StartCoroutine(Rotate(360 * _numRolls + 120,Vector3.up + Vector3.right + Vector3.back));
    //top => StartCoroutine(Rotate(360 * _numRolls - 120,Vector3.up + Vector3.right + Vector3.back));
    
    // ================================================== front e back (bella)
    // front => StartCoroutine(Rotate(360 * _numRolls, Vector3.up + Vector3.right));
    // back => StartCoroutine(Rotate(360 * _numRolls + 180, Vector3.up + Vector3.right));
    
    //NON USATE
    
    // ================================================== right e bottom
    // right => StartCoroutine(Rotate(360 * _numRolls + 120,Vector3.up + Vector3.left + Vector3.forward));
    // bottom => StartCoroutine(Rotate(360 * _numRolls - 120,Vector3.up + Vector3.left + Vector3.forward));
    // front => StartCoroutine(Rotate(360 * _numRolls,Vector3.up + Vector3.left + Vector3.forward));

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
