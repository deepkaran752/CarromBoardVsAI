using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ClassType
{
    Queen,
    Black_Pawn,
    White_Pawn,
    Striker
};
public class PawnClass : MonoBehaviour
{
    public ClassType CLASS_;

    public int ScoreSetter_()
    {
        switch (CLASS_)
        {
            case ClassType.Queen:
                return 2;
            case ClassType.Black_Pawn:
                return 1;
            case ClassType.White_Pawn:
                return 1;
            default:
                return 0;
        }
    }
}
