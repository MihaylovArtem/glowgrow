using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    public enum GameState {
        MainMenu,
        Playing,
        GameOver,
        PreparingUI,
        BetweenStage,
        Tutorial
    }

    public static int totalScore;
    public static int recordScore;
    public static float multiplayer;

    public static GameState gameState;
    public static int stage;

    public GameObject arrowObject;
    private GameObject canvas;
    private GameObject currentPlayer;

    public GameObject scoreLabel;
    private GameObject scoreLabelClone;


    public GameObject endOfStageLabel;
    public GameObject gameBackgroundObject;

    public GameObject mainMenuGameObject;
    private float menuElementsSpeed = 0.03f;
    public GameObject newStageLabel;
    public GameObject pattern;

    public GameObject playerPrefab;

    private GameObject pressUpText;
    public GameObject pressUpTextPrefab;
    private GameObject helpText;
    private float score;
    private float time;
    public GameObject tutorialTextPrefab;
    public GameObject tutorialTextPrefab2;
    // Use this for initialization

    private void Start() {
        pattern = Instantiate(pattern);
        canvas = GameObject.Find("Canvas");
        pattern.GetComponent<Pattern>().tutorialTextPrefab = tutorialTextPrefab;
        pattern.GetComponent<Pattern>().tutorialTextPrefab2 = tutorialTextPrefab2;

        pressUpText = Instantiate(pressUpTextPrefab);
        pressUpText.transform.SetParent(canvas.transform, false);
        ColorPalette.InitPalleteNum(2);
        Camera.main.backgroundColor = ColorPalette.background1Color;
        gameBackgroundObject.GetComponent<SpriteRenderer>().color = ColorPalette.background2Color;
        mainMenuGameObject.GetComponent<SpriteRenderer>().color = ColorPalette.background2Color;
        arrowObject.GetComponent<SpriteRenderer>().color = ColorPalette.background1Color;

        gameState = GameState.MainMenu;
        mainMenuGameObject.transform.position = new Vector3(0, 0, 1);
        gameBackgroundObject.transform.position = new Vector3(0,
            2*Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y, 0);
    }


    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyUp(KeyCode.UpArrow) && gameState == GameState.MainMenu) {
            StartCoroutine(GoToGameScreen());
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) && gameState == GameState.GameOver) {
            if (PlayerPrefs.GetInt("TutorialCompleted") != 1) {
                StartTutorial(true);
            }
            else {
                StartNewGame();
            }
        }
        if (Input.GetKeyUp(KeyCode.DownArrow) && gameState == GameState.GameOver) {
            StartCoroutine(GoToMainMenuScreen());
        }
        if (gameState == GameState.Playing || gameState == GameState.GameOver)
        {
            scoreLabelClone.GetComponent<Text>().text = "Record: " + recordScore + "\n" + totalScore.ToString() + " PTS\nx" + multiplayer;
        }
    }

    private IEnumerator GoToGameScreen() {
        pressUpText.SetActive(false);
        gameState = GameState.PreparingUI;
        while (gameBackgroundObject.transform.position.y > 0) {
            yield return new WaitForEndOfFrame();
            float yPosChange = menuElementsSpeed*(gameBackgroundObject.transform.position.y + 1);
            gameBackgroundObject.transform.position -= new Vector3(0, yPosChange, 0);
            mainMenuGameObject.transform.position -= new Vector3(0, yPosChange, 0);
        }
        if (PlayerPrefs.GetInt("TutorialCompleted") != 1) {
            StartTutorial(false);
        }
        else {
            StartNewGame();
        }
    }

    private IEnumerator GoToMainMenuScreen() {
        Destroy(scoreLabelClone);
        while (mainMenuGameObject.transform.position.y < 0) {
            gameState = GameState.PreparingUI;
            yield return new WaitForEndOfFrame();
            float yPosChange = menuElementsSpeed*(gameBackgroundObject.transform.position.y + 1);
            gameBackgroundObject.transform.position += new Vector3(0, yPosChange, 0);
            mainMenuGameObject.transform.position += new Vector3(0, yPosChange, 0);
        }
        pressUpText.SetActive(true);
        gameState = GameState.MainMenu;
    }

    public void StartTutorial(bool again) {
        gameState = GameState.Tutorial;
        pattern.GetComponent<Pattern>().StartTutorial(again);
        StartCoroutine(CreatePlayer());
    }

    private void StartNewGame() {
        gameState = GameState.Playing;
        time = 0;
        stage = 1;
        DestroyImmediate(helpText);
        totalScore = 0;
        multiplayer = 1f;
        recordScore = PlayerPrefs.GetInt("recordScore");
        StartCoroutine(CreatePlayer());
        Destroy(scoreLabelClone);
        scoreLabelClone = Instantiate(scoreLabel);
        scoreLabelClone.transform.SetParent(canvas.transform, false);
        BeginStage();
    }
    public void EndStage() {
        gameState = GameState.BetweenStage;
            GameObject endOfStageClone = Instantiate(endOfStageLabel);
            endOfStageClone.transform.SetParent(canvas.transform, false);
        endOfStageClone.GetComponent<Text>().text="STAGE "+stage+" ENDED";
            Destroy(endOfStageClone, 3.0f);
            Invoke("BeginStage", 3.0f);
            stage++;
            multiplayer+=0.2f;
        pattern.GetComponent<Pattern>().StopAllPatternCoroutines();
    }

    public void BeginStage()
    {
        gameState = GameState.Playing;
        int patternNum = 3;
        pattern.GetComponent<Pattern>().InitPatternWithNum(patternNum, 0.0f);
        GameObject newStageClone = Instantiate(newStageLabel);
        newStageClone.transform.SetParent(canvas.transform, false);
        newStageClone.GetComponent<Text>().text = "STAGE " + stage;
        Destroy(newStageClone, 5.0f);
    }

    public IEnumerator CreatePlayer() {
        Destroy(currentPlayer);
        currentPlayer = Instantiate(playerPrefab);
        currentPlayer.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        currentPlayer.GetComponent<SpriteRenderer>().color = ColorPalette.playerColor;
        currentPlayer.GetComponent<Player>().currentGlowColor = ColorPalette.bullet1Color;
        while (currentPlayer.transform.localScale.x <= 1) {
            currentPlayer.transform.localScale += new Vector3(Time.deltaTime*5, Time.deltaTime*5, Time.deltaTime*5);
            currentPlayer.transform.GetChild(0).localScale += new Vector3(Time.deltaTime*5, Time.deltaTime*5,
                Time.deltaTime*5);
            yield return new WaitForEndOfFrame();
        }
        Destroy(currentPlayer);
        currentPlayer = Instantiate(playerPrefab);
        currentPlayer.GetComponent<SpriteRenderer>().color = ColorPalette.playerColor;
        currentPlayer.GetComponent<Player>().currentGlowColor = ColorPalette.bullet1Color;
    }

    public void GameOver()
    {
        PlayerPrefs.SetInt("recordScore", 3);
        pattern.GetComponent<Pattern>().StopAllPatternCoroutines();
        gameState = GameState.GameOver;
        Invoke("InstantiateHelpText",2.0f);
    }

    void InstantiateHelpText() {

        helpText = Instantiate(pressUpTextPrefab);
        helpText.transform.SetParent(canvas.transform, false);
        helpText.GetComponent<Text>().text = "Press up to restart\n\nPress down to main menu";
    }
}