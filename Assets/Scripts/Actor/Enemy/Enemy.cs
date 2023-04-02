using System.Linq;
using UnityEngine;

public class Enemy : ActorMovement
{
    protected override void OnEnable()
    {
        base.OnEnable();
        PlayerMovement.OnPlayerMove += TakeDecision;
    }

    protected override void OnDisable()
    {
        base.OnEnable();
        PlayerMovement.OnPlayerMove -= TakeDecision;
    }

    private void TakeDecision()
    {
        // Enemy logic goes here 
        MoveRandom();
    }

    // To be deleted
    private void MoveRandom()
    {
        int[] moveList = { 0, 1, 2, 3};
        moveList = moveList.OrderBy(x => Random.Range(0,4)).ToArray();
        foreach(int move in moveList)
        {
            if (p_moved)
                return;

            switch (Random.Range(0, 4))
            {
                case 0:
                    MoveUp();
                    break;
                case 1:
                    MoveDown();
                    break;
                case 2:
                    MoveLeft();
                    break;
                case 3:
                    MoveRight();
                    break;
            }
        }
            // Old code
            //do
            //{
            //    switch (Random.Range(0, 4))
            //    {
            //        case 0:
            //            MoveUp();
            //            break;
            //        case 1:
            //            MoveDown();
            //            break;
            //        case 2:
            //            MoveLeft();
            //            break;
            //        case 3:
            //            MoveRight();
            //            break;
            //    }
            //} while (!p_moved);
        }
}
