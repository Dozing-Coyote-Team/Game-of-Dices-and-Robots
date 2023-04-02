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

    [Header("Settings")]
    [SerializeField] private int _numRolls = 3;
    [SerializeField] private float _animDuration = 3f;
    [SerializeField] private AnimationCurve _animationCurve;
    
    [Header("References")]
    [SerializeField] List<Transform> _faceIndicators;
    [Header("Rotation references")]
    [SerializeField] private List<UIDieModelRotation> _frontRotations;
    [SerializeField] private UIDieModelRotation _backRotation;
    [SerializeField] private UIDieModelRotation _rightRotation;
    [SerializeField] private UIDieModelRotation _leftRotaion;
    [SerializeField] private UIDieModelRotation _upRotation;
    [SerializeField] private UIDieModelRotation _bottomRotation;

    // private int _currentResult = 1;

    private float _diceRadius;
    //--------------------------- public methods
    public void RollTo(int newResult)
    {
        UIDieModelRotation rotation = EvaluateRotationTo(newResult);

        StartCoroutine(Rotate(360 * _numRolls + rotation.RotationOffset,rotation.Axis));
    }

    private UIDieModelRotation EvaluateRotationTo(int newResult)
    {

        Vector3 newResultPos = _faceIndicators[newResult -1].position;

        if (newResultPos.y > transform.position.y+_diceRadius/2)
            return _upRotation;
        else if (newResultPos.y < transform.position.y-_diceRadius/2)
            return _bottomRotation;
        else if (newResultPos.x > transform.position.x+_diceRadius/2)
            return _rightRotation;
        else if (newResultPos.x < transform.position.x-_diceRadius/2)
            return _leftRotaion;
        else if(newResultPos.z > transform.position.z + _diceRadius/2)
            return _backRotation;
        else
            return _frontRotations[Random.Range(0, _frontRotations.Count)];
    }

    public Action OnRollAnimEnd;
    //--------------------------- private methods
    
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
            
            transform.RotateAround(transform.position,aroundAxis,frameRot);

            previousRot = currentRot;
            timeElapsed += Time.deltaTime;
            
            yield return null;
        }
    }

    private void Awake()
    {
        _diceRadius = Vector3.Distance(transform.position, _faceIndicators[0].position);
    }
}



// USATE
// ================================================== left e bottom 
// bottom => StartCoroutine(Rotate(360 * _numRolls + 120,Vector3.up + Vector3.right + Vector3.forward));
// left => StartCoroutine(Rotate(360 * _numRolls - 120,Vector3.up + Vector3.right + Vector3.forward));
    
// ================================================== right e top
//right => StartCoroutine(Rotate(360 * _numRolls + 120,Vector3.up + Vector3.right + Vector3.back));
// top => StartCoroutine(Rotate(360 * _numRolls - 120,Vector3.up + Vector3.right + Vector3.back));
    
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