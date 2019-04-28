using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour {

    public static GameManager instance = null;

    private void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

#if UNITY_STANDALONE
        quitButton.gameObject.SetActive(true);
#endif
    }

    private void Start() {
        OpenPlay();
    }

    [SerializeField]
    private TextMeshProUGUI LivesAmountText;

    [SerializeField]
    private Button GruntButton;
    [SerializeField]
    private Button TankButton;
    [SerializeField]
    private Button SprintlingButton;

    public GameObject MenuScene;

    public RectTransform BG;
    public RectTransform Play;
    public RectTransform BuyUI;
    public RectTransform Levels;
    public RectTransform LevelResult;

    public RectTransform quitButton;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI levelIndexText;

    public int GruntCost = 2;
    public int TankCost = 5;
    public int SprintlingCost = 1;

    private int lives = 0;

    public void OpenBuyUI() {
        BG.gameObject.SetActive(false);
        Play.gameObject.SetActive(false);
        BuyUI.gameObject.SetActive(true);
        Levels.gameObject.SetActive(false);
        LevelResult.gameObject.SetActive(false);
    }

    public void OpenLevels() {
        BG.gameObject.SetActive(true);
        Play.gameObject.SetActive(false);
        BuyUI.gameObject.SetActive(false);
        Levels.gameObject.SetActive(true);
        LevelResult.gameObject.SetActive(false);
    }

    public void OpenPlay() {
        Level[] levels = FindObjectsOfType<Level>();
        for (int i = 0; i < levels.Length; i++) {
            levels[i].RemoveLevel();
        }

        BG.gameObject.SetActive(true);
        Play.gameObject.SetActive(true);
        BuyUI.gameObject.SetActive(false);
        Levels.gameObject.SetActive(false);
        LevelResult.gameObject.SetActive(false);
        MenuScene.SetActive(true);
    }

    public void OpenLevelResults(int score, int bestScore) {
        levelIndexText.text = "Completed Level " + LevelManager.instance.currentLevel.LevelIndex;
        scoreText.text = "Score: " + score;
        bestScoreText.text = "Best: " + bestScore;

        BG.gameObject.SetActive(true);
        Play.gameObject.SetActive(false);
        BuyUI.gameObject.SetActive(false);
        Levels.gameObject.SetActive(false);
        LevelResult.gameObject.SetActive(true);

        PawnManager.instance.ClearPawns();
    }

    public void PressedQuit() {
#if UNITY_WEBGL || UNITY_ANDROID || UNITY_IOS
        return;
#endif

        Application.Quit();
    }

    public void OpenLevel(int levelIndex) {
        BG.gameObject.SetActive(false);
        Play.gameObject.SetActive(false);
        BuyUI.gameObject.SetActive(false);
        Levels.gameObject.SetActive(false);
        LevelResult.gameObject.SetActive(false);

        CameraManager.instance.transform.localPosition = Vector3.zero;
        LevelManager.instance.LoadLevel(levelIndex);
        MenuScene.SetActive(false);
    }

    public void PressedGruntButton() {
        TakeLives(GruntCost);
        PawnManager.instance.SpawnGrunt();
    }

    public void PressedTankButton() {
        TakeLives(TankCost);
        PawnManager.instance.SpawnTank();
    }

    public void PressedSprintlingButton() {
        TakeLives(SprintlingCost);
        PawnManager.instance.SpawnSprintling();
    }

    public void CheckIfLost() {
        if (Lives > 0)
            return;

        if (PawnManager.instance.alivePawns.Count > 0)
            return;

        OpenPlay();
    }

    public void TakeLives(int amount) {
        Lives -= amount;
    }

    public int Lives {
        get {
            return lives;
        }
        set {
            lives = value;
            lives = Mathf.Clamp(lives, 0, LevelManager.instance.currentLevel.StartLives);
            LivesAmountText.text = "" + lives;

            GruntButton.interactable = lives >= GruntCost;
            GruntButton.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "cost: " + GruntCost;
            TankButton.interactable = lives >= TankCost;
            TankButton.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "cost: " + TankCost;
            SprintlingButton.interactable = lives >= SprintlingCost;
            SprintlingButton.GetComponentsInChildren<TextMeshProUGUI>()[1].text = "cost: " + SprintlingCost;
        }
    }
}
