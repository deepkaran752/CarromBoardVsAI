using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetection : MonoBehaviour
{
    GameController Gc;
    //PawnClass PClass;
    public int Score_Human = 0;
    public int Score_AI = 0;

    void Start()
    {
        Gc = FindObjectOfType<GameController>();
    }
    // Start is called before the first frame update
    public void OnTriggerEnter2D(Collider2D other) {
        GameObject collidedObject = other.gameObject;
        PawnClass collidedPawn = other.gameObject.GetComponent<PawnClass>();

        if (collidedObject != null)
        {
            if (Gc.isPlayer == true)
            {
                switch (collidedPawn.CLASS_)
                {
                    case ClassType.Queen:
                        Score_Human += 2;
                        Destroy(collidedObject);
                        break;
                    case ClassType.Black_Pawn:
                        Score_Human += 1;
                        Destroy(collidedObject);
                        break;
                    case ClassType.White_Pawn:
                        Score_Human += 1;
                        Destroy(collidedObject);
                        break;
                    default:
                        Score_Human += 0;
                        break;
                }
            }else if(Gc.isPlayer == false)
            {
                //AI
                switch (collidedPawn.CLASS_)
                {
                    case ClassType.Queen:
                        Score_AI += 2;
                        Destroy(collidedObject);
                        break;
                    case ClassType.Black_Pawn:
                        Destroy(collidedObject);
                        Score_AI += 1;
                        break;
                    case ClassType.White_Pawn:
                        Score_AI += 1;
                        Destroy(collidedObject);
                        break;
                    default:
                        Score_AI += 0;
                        break;
                }
            }
        }
        else
        {
            Debug.Log("No Object passed!");
        }
    }
}
