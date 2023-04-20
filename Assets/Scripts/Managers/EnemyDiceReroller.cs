using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiceReroller : MonoBehaviour
{
    private void Start()
    {
        RerollManager.Instance.OnReroll += Reroll;
    }

    public void Reroll()
    {
        //todo
        //enemy ia
        Debug.Log("enemy rerolled");
    }
}
