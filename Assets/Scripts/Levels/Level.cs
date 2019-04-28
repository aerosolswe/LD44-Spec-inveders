using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {

    public Transform StartPoint;
    public Transform EndPoint;

    public int LevelIndex = 0;
    public int StartLives = 10;

    private void Start() {
        LevelManager.instance.currentLevel = this;
        GameManager.instance.Lives = StartLives;
        PawnManager.instance.alivePawns.Clear();
        GameManager.instance.OpenBuyUI();
    }

    public int CalculateScore() {
        int livesLeft = GameManager.instance.Lives;

        int pawnsAliveScore = 0;
        foreach (Pawn p in PawnManager.instance.alivePawns) {
            pawnsAliveScore += p.pawnInfo.ScoreAmount;
        }

        int livesLeftScore = 100 * livesLeft;

        int score = pawnsAliveScore + livesLeftScore;

        if (score > GetBestScore()) {
            PlayerPrefs.SetInt("LEVEL" + LevelIndex + "_HS", score);
            PlayerPrefs.Save();
        }

        return score;
    }

    public int GetBestScore() {
        return PlayerPrefs.GetInt("LEVEL" + LevelIndex + "_HS", 0);
    }

    public void RemoveLevel() {
        UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync("Level" + LevelIndex);
        Destroy(this.gameObject);
    }
}
