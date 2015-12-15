using System.Collections;
using UnityEngine;
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
    public static float time;

    public GameObject arrowObject;
    public AudioManager audioManager;
    private GameObject canvas;
    private GameObject currentPlayer;


    public GameObject endOfStageLabel;
    public GameObject gameBackgroundObject;
    public GameObject gameNameText;
    private GameObject helpText;

    public GameObject mainMenuGameObject;
    private float menuElementsSpeed = 0.03f;
    public GameObject newStageLabel;
    public GameObject pattern;

    public GameObject playerPrefab;

    private GameObject pressUpText;
    public GameObject pressUpTextPrefab;
    private float score;
    public GameObject scoreLabel;
    private GameObject scoreLabelClone;
    public GameObject tutorialTextPrefab;
    public GameObject tutorialTextPrefab2;
    public GameObject colorManagerPrefab;
    public static GameObject colorManager;

    private bool tutorialCompleted = false;

    // Use this for initialization

    private void Start()
    {
        audioManager.PlayMusic(1);
        pattern = Instantiate(pattern);
        colorManager = Instantiate(colorManagerPrefab);
        canvas = GameObject.Find("Canvas");
        pattern.GetComponent<Pattern>().tutorialTextPrefab = tutorialTextPrefab;
        pattern.GetComponent<Pattern>().tutorialTextPrefab2 = tutorialTextPrefab2;

        pressUpText = Instantiate(pressUpTextPrefab);
        pressUpText.transform.SetParent(canvas.transform, false);
        ColorPalette.InitPalleteNum(2);
        GameObject.Find("LeftArrow").GetComponent<SpriteRenderer>().color = ColorPalette.bullet1Color;
        GameObject.Find("RightArrow").GetComponent<SpriteRenderer>().color = ColorPalette.bullet2Color;
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
        if (Input.GetKeyUp(KeyCode.UpArrow) && gameState == GameState.GameOver && helpText) {
            if (!tutorialCompleted)
            {
                StartTutorial(true);
            }
            else {
                StartNewGame();
            }
        }
//        if (Input.GetKeyUp(KeyCode.DownArrow) && gameState == GameState.GameOver) {
//            StartCoroutine(GoToMainMenuScreen());
//        }
        if (gameState == GameState.Playing) time += Time.deltaTime;
        if (gameState == GameState.Playing || gameState == GameState.GameOver && tutorialCompleted)
        {
            Debug.Log(PlayerPrefs.GetInt("TutorialCompleted"));
            scoreLabelClone.GetComponent<Text>().text = "Record: " + recordScore + " PTS\n" + totalScore + " PTS\nx" +
                                                        multiplayer;
        }
    }

    private IEnumerator GoToGameScreen() {
        pressUpText.SetActive(false);
        gameNameText.SetActive(false);
        gameState = GameState.PreparingUI;
        while (gameBackgroundObject.transform.position.y > 0) {
            yield return new WaitForEndOfFrame();
            float yPosChange = menuElementsSpeed*(gameBackgroundObject.transform.position.y + 1);
            gameBackgroundObject.transform.position -= new Vector3(0, yPosChange, 0);
            mainMenuGameObject.transform.position -= new Vector3(0, yPosChange, 0);
        }
        if (!tutorialCompleted) {
            StartTutorial(false);
        }
        else {
            StartNewGame();
        }
    }

    private IEnumerator GoToMainMenuScreen() {
        Destroy(helpText);
        Destroy(scoreLabelClone);
        while (mainMenuGameObject.transform.position.y < 0 && mainMenuGameObject.transform.position.y<Screen.height*2)
        {
            gameState = GameState.PreparingUI;
            yield return new WaitForEndOfFrame();
            float yPosChange = menuElementsSpeed*(gameBackgroundObject.transform.position.y + 1);
            gameBackgroundObject.transform.position += new Vector3(0, yPosChange, 0);
            mainMenuGameObject.transform.position += new Vector3(0, yPosChange, 0);
        }
        gameBackgroundObject.transform.position = new Vector3(0, Screen.height*2, 0);
        mainMenuGameObject.transform.position = new Vector3(0, 0, 1);
        pressUpText.SetActive(true);
        gameNameText.SetActive(true);
        gameState = GameState.MainMenu;
    }

    public void StartTutorial(bool again)
    {
        if (again)
        {
            colorManager.GetComponent<ColorPalette>().ChangeAllObjectsToMatchPallete(2);
            audioManager.ContinueMusic();
            Destroy(helpText);

            foreach (var player in GameObject.FindGameObjectsWithTag("Row"))
            {
                Destroy(player);
            }
        }
        gameState = GameState.Tutorial;
        pattern.GetComponent<Pattern>().StartTutorial(again);
        StartCoroutine(CreatePlayer());
    }

    public void StartNewGame() {
        tutorialCompleted = true;
        foreach (var player in GameObject.FindGameObjectsWithTag("Player")) {
            Destroy(player);
        }
        foreach (var player in GameObject.FindGameObjectsWithTag("Row"))
        {
            Destroy(player);
        }
        time = 0;
        int randomPallete = Random.Range(1, 5);
        colorManager.GetComponent<ColorPalette>().ChangeAllObjectsToMatchPallete(randomPallete);
        audioManager.PlayMusic(0);
        gameState = GameState.Playing;
        time = 0;
        stage = 1;
        Destroy(helpText);
        totalScore = 0;
        multiplayer = 1f;
        recordScore = PlayerPrefs.GetInt("recordScore");
        StartCoroutine(CreatePlayer());
        Destroy(scoreLabelClone);
        scoreLabelClone = Instantiate(scoreLabel);
        scoreLabelClone.transform.SetParent(canvas.transform, false);
        BeginStage();
    }

    public void EndStage()
    {
        colorManager.GetComponent<ColorPalette>().ChangeAllObjectsToMatchPallete(0);
        audioManager.PauseMusic();
        gameState = GameState.BetweenStage;
        GameObject endOfStageClone = Instantiate(endOfStageLabel);
        endOfStageClone.transform.SetParent(canvas.transform, false);
        endOfStageClone.GetComponent<Text>().text = "STAGE " + stage + " ENDED";
        Destroy(endOfStageClone, 3.0f);
        Invoke("BeginStage", 3.0f);
        stage++;
        multiplayer += 0.2f;
        pattern.GetComponent<Pattern>().StopAllPatternCoroutines();
    }

    public void BeginStage()
    {
        int randomPallete = Random.Range(1, 5);
        colorManager.GetComponent<ColorPalette>().ChangeAllObjectsToMatchPallete(randomPallete);
        audioManager.ContinueMusic();
        gameState = GameState.Playing;
        pattern.GetComponent<Pattern>().CallRandomPattern();
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

    public void GameOver() {
        time = 0;
        audioManager.PauseMusic();
        PlayerPrefs.SetInt("recordScore",recordScore);
        pattern.GetComponent<Pattern>().StopAllPatternCoroutines();
        gameState = GameState.GameOver;
        Invoke("InstantiateHelpText", 2.0f);
        colorManager.GetComponent<ColorPalette>().ChangeAllObjectsToMatchPallete(0);
    }

    private void InstantiateHelpText() {
        helpText = Instantiate(pressUpTextPrefab);
        helpText.transform.name = "Help text";
        helpText.transform.SetParent(canvas.transform, false);
        helpText.GetComponent<Text>().text = "Press up to restart";
    }
}