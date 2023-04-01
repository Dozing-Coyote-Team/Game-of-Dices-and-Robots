using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceTest : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(TestCor());
    }

    private IEnumerator TestCor()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            DiceManager.Instance.RollDie(Random.Range(0,5));
            yield return new WaitForSeconds(3f);
            
            Debug.Log("Is any dice rolling: "+DiceManager.Instance.IsAnyDiceRolling());

            Debug.Log("Results: "+ IntListToString(DiceManager.Instance.GetResults()));
            Debug.Log("D0:"+ DiceManager.Instance.GetResult(0) + 
                      " D1:"+ DiceManager.Instance.GetResult(1) +
                      " D2:"+ DiceManager.Instance.GetResult(2) +
                      " D3:"+ DiceManager.Instance.GetResult(3) +
                      " D4:"+ DiceManager.Instance.GetResult(4)
                      );
        }
    }

    private string IntListToString(List<int?> list)
    {
        string res="[";
        foreach (int? val in list)
        {
            if (val != null)
                res += " "+val.ToString()+" ";
            else
                res += " null ";
        }
        res += "]";
        return res;
    }
}
