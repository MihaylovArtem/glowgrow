using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    public enum GameState {
        MainMenu,
        Playing,
        GameOver,
        PreparingUI,
        PreparingNewStage,
        Tutorial
    }

    public static GameState gameState;
    public static int stage;

    public GameObject arrowObject;

    public GameObject endOfStageLabel;
    public GameObject gameBackgroundObject;

    public GameObject mainMenuGameObject;
    private float menuElementsSpeed = 0.03f;
    public GameObject newStageLabel;
    public GameObject pattern;

    public GameObject playerPrefab;
    private GameObject currentPlayer;

    private GameObject pressUpText;
    public GameObject pressUpTextPrefab;
    private float score;
    private float time;
    public GameObject tutorialTextPrefab;
    public GameObject tutorialTextPrefab2;
    // Use this for initialization

    private void Start() {
        pattern = Instantiate(pattern);
        pressUpText = Instantiate(pressUpTextPrefab);
        pressUpText.transform.SetParent(GameObject.Find("Canvas").transform, false);
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
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) && gameState == GameState.MainMenu) {
            StartCoroutine(GoToGameScreen());
        }
    }

    IEnumerator GoToGameScreen() {
        pressUpText.SetActive(false);
        gameState = GameState.PreparingUI;
        while (gameBackgroundObject.transform.position.y > 0) {
            yield return new WaitForEndOfFrame();
            float yPosChange = menuElementsSpeed * (gameBackgroundObject.transform.position.y + 1);
            gameBackgroundObject.transform.position -= new Vector3(0, yPosChange, 0);
            mainMenuGameObject.transform.position -= new Vector3(0, yPosChange, 0);
        }
//        if (PlayerPrefs.GetInt("TutorialCompleted") != 1) {
            StartTutorial();
//        }
//        else {
//            StartNewGame();
//        }
    }
    IEnumerator GoToMainMenuScreen()
    {
        while (mainMenuGameObject.transform.position.y < 0)
        {
            gameState = GameState.PreparingUI;
            yield return new WaitForEndOfFrame();
            float yPosChange = menuElementsSpeed * (gameBackgroundObject.transform.position.y + 1);
            gameBackgroundObject.transform.position += new Vector3(0, yPosChange, 0);
            mainMenuGameObject.transform.position += new Vector3(0, yPosChange, 0);
        }
        pressUpText.SetActive(true);
        gameState = GameState.MainMenu;
    }

    public void StartTutorial() {
        gameState = GameState.Tutorial;
        pattern.GetComponent<Pattern>().tutorialTextPrefab = tutorialTextPrefab;
        pattern.GetComponent<Pattern>().tutorialTextPrefab2 = tutorialTextPrefab2;
        pattern.GetComponent<Pattern>().StartTutorial(false);
        StartCoroutine(CreatePlayer());
    }
    private void StartNewGame()
    {
        gameState = GameState.Playing;
        time = 0;
        stage = 1;
        score = 0;
        StartCoroutine(CreatePlayer());
    }

    public void EndStage() {
            GameObject endOfStageClone = Instantiate(endOfStageLabel);
            endOfStageClone.transform.SetParent(GameObject.Find("Canvas").transform, false);
            Destroy(endOfStageClone, 3.0f);
            Invoke("BeginStage", 3.0f);
            stage++;
    }

    public void BeginStage() {
        GameObject newStageClone = Instantiate(newStageLabel);
        newStageClone.transform.SetParent(GameObject.Find("Canvas").transform, false);
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
            currentPlayer.transform.localScale += new Vector3(Time.deltaTime * 5, Time.deltaTime * 5, Time.deltaTime * 5);
            currentPlayer.transform.GetChild(0).localScale += new Vector3(Time.deltaTime * 5, Time.deltaTime * 5, Time.deltaTime * 5);
            yield return new WaitForEndOfFrame();
        }
        Destroy(currentPlayer);
        currentPlayer = Instantiate(playerPrefab);
        currentPlayer.GetComponent<SpriteRenderer>().color = ColorPalette.playerColor;
        currentPlayer.GetComponent<Player>().currentGlowColor = ColorPalette.bullet1Color;
    }
}