using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pawn", menuName = "Pawns/Pawn", order = 1)]
public class PawnInfo : ScriptableObject {
    public int Health = 100;
    public int ScoreAmount = 50;
}
