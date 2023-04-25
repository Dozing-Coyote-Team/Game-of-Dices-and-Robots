using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopManager : Singleton<GameLoopManager>
{
    public void GameOver()
    {
        Debug.Log("GAME OVER");
    }
}
