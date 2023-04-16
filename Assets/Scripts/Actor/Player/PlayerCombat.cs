using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public void CheckEnemies(Vector2Int index)
    {
        CombatManager.Instance.ClearQueue();

        List<Enemy> neighbourEnemies = GridManager.Instance.GetNeighbourEnemies(index);
        for (int i = 0; i < neighbourEnemies.Count; i++)
        {
            CombatManager.Instance.RegisterEnemy(neighbourEnemies[i]);
        }

        if (neighbourEnemies.Count > 0)
            CombatManager.Instance.StartBattle();
        else
            CameraManager.Instance.ActivePlayerCamera();
    }
}
