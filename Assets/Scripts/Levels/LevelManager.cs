using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

    public static LevelManager instance = null;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() {
    }

    public Level currentLevel;

    public void LoadLevel(int levelIndex) {
        UnityEngine.SceneManagement.SceneManager.LoadScene("level" + levelIndex, UnityEngine.SceneManagement.LoadSceneMode.Additive);
    }

}
