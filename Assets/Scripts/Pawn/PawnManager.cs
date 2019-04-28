using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnManager : MonoBehaviour {

    public static WaitForSeconds spawnDelay = new WaitForSeconds(0.3f);

    public static PawnManager instance = null;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }


    public Transform pawnsContainer;

    public Pawn gruntObject;
    public Pawn tankObject;
    public Pawn sprintlingObject;

    public List<Pawn> alivePawns = new List<Pawn>();

    public void SpawnGrunt() {
        Pawn pawn = InstantiatePawn(gruntObject);
        pawn.StartMoving();
        alivePawns.Add(pawn);
    }

    public void SpawnTank() {
        Pawn pawn = InstantiatePawn(tankObject);
        pawn.StartMoving();
        alivePawns.Add(pawn);
    }

    public void SpawnSprintling() {
        Pawn pawn = InstantiatePawn(sprintlingObject);
        pawn.StartMoving();
        alivePawns.Add(pawn);
    }

    public void ClearPawns() {
        foreach (Pawn p in pawnsContainer.GetComponentsInChildren<Pawn>()) {
            if (p != null) {
                Destroy(p.gameObject);
            }
        }

        alivePawns.Clear();
    }

    public Pawn InstantiatePawn(Pawn pawn) {
        Pawn p = Instantiate(pawn, pawnsContainer);
        p.Init();
        p.agent.Warp(LevelManager.instance.currentLevel.StartPoint.position);
        p.gameObject.SetActive(true);
        p.moveTarget = LevelManager.instance.currentLevel.EndPoint;

        return p;
    }
}
