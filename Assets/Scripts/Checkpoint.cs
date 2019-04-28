using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

    public enum CheckpointType {
        Start, End
    }

    public CheckpointType type;
    public LayerMask pawnLayer;

    private bool won = false;

    private void OnDrawGizmos() {
        if (type == CheckpointType.Start) {
            Gizmos.color = new Color(0.2f, 0.3f, 1, 0.2f);
        } else {
            Gizmos.color = new Color(1f, 0.3f, 0.2f, 0.2f);
        }

        Gizmos.DrawSphere(transform.position, 0.5f);
    }

    private void OnTriggerEnter(Collider other) {
        if (type != CheckpointType.End)
            return;

        if (other.transform.parent.tag == "Pawn") {
            other.gameObject.SetActive(false);

            Win();
        }
    }

    public void Win() {
        if (won)
            return;
        won = true;

        Level level = LevelManager.instance.currentLevel;
        int score = level.CalculateScore();
        int bestScore = level.GetBestScore();
        GameManager.instance.OpenLevelResults(score, bestScore);
    }
}
