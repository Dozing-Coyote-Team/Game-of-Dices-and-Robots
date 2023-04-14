using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public void CheckEnemies(Vector2Int index)
    {
        CombatManager.Instance.ClearQueue();

        List<Enemy> result = GridManager.Instance.GetNeighbourEnemies(index);
        for (int i = 0; i < result.Count; i++)
        {
            CombatManager.Instance.RegisterEnemy(result[i]);
        }

        if (result.Count > 0)
            CombatManager.Instance.StartBattle();
        else
            CameraManager.Instance.ActivePlayerCamera();
    }
}
