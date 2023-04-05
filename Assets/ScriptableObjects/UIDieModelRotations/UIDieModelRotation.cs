using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UIDieModelRotation", menuName = "ScriptableObjects/UIDieModelRotation", order = 1)]
public class UIDieModelRotation : ScriptableObject
{
    public int RotationOffset;
    public Vector3 Axis;
}
