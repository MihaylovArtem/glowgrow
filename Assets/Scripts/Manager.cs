using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    public enum GameState {
        MainMenu,
        Playing,
        GameOver,
        PreparingUI,
        PreparingNewStage
    }

    public GameObject arrowObject;
    public GameObject gameBackgroundObject;

    public GameObject endOfStageLabel;
    public GameObject newStageLabel;
    public static GameState gameState;

    public Text gameTimeText;
    public GameObject mainMenuGameObject;
    private float menuElementsSpeed = 0.03f;

    public GameObject playerGameObject;
    private float score;
    public static int stage;
    private float time;
    // Use this for initialization

    private void Start() {
        ColorPalette.InitPalleteNum(2);
        Camera.main.backgroundColor = ColorPalette.background1Color;
        gameBackgroundObject.GetComponent<SpriteRenderer>().color = ColorPalette.background2Color;
        mainMenuGameObject.GetComponent<SpriteRenderer>().color = ColorPalette.background2Color;
        arrowObject.GetComponent<SpriteRenderer>().color = ColorPalette.background1Color;
        playerGameObject.GetComponent<SpriteRenderer>().color = ColorPalette.playerColor;
        playerGameObject.GetComponent<Player>().currentGlowColor = ColorPalette.bullet1Color;

        gameState = GameState.MainMenu;
        mainMenuGameObject.transform.position = new Vector3(0, 0, 1);
        gameBackgroundObject.transform.position = new Vector3(0,
            2*Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y, 0);

        score = 0;
        time = 0;
        stage = 0;
    }

    //private void StartGame()
    //{
    //    isGameStarted = true;
    //    time = 0;
    //    stage = 1;
    //    score = 0;
    //    Instantiate(playerGameObject, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
    //    Instantiate(gameTimeText);
    //    gameTimeText.transform.position = new Vector3(Screen.width/2, Screen.height/2, 0);
    //    //gameTimeText.
    //}

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyUp(KeyCode.UpArrow) && gameState == GameState.MainMenu) {
            gameState = GameState.PreparingUI;
        }

        if (gameState == GameState.PreparingUI) {
            if (gameBackgroundObject.transform.position.y > 0) {
                float yPosChange = menuElementsSpeed*(gameBackgroundObject.transform.position.y + 1);
                gameBackgroundObject.transform.position -= new Vector3(0, yPosChange, 0);
                mainMenuGameObject.transform.position -= new Vector3(0, yPosChange, 0);
            }
            else {
                gameState = GameState.Playing;
            }
        }
    }

    public void EndStage() {

        if (gameState != GameState.PreparingNewStage) {
            gameState = GameState.PreparingNewStage;
            GameObject endOfStageClone = Instantiate(endOfStageLabel);
            endOfStageClone.transform.SetParent(GameObject.Find("Canvas").transform, false);
            Destroy(endOfStageClone, 3.0f);
            Invoke("BeginNextStage", 3.0f);
        }
    }

    public void BeginNextStage() {
        if (gameState != GameState.PreparingUI) {
            stage++;
            gameState = GameState.PreparingUI;
            GameObject newStageClone = Instantiate(newStageLabel);
            newStageClone.transform.SetParent(GameObject.Find("Canvas").transform, false);
            newStageClone.GetComponent<Text>().text = "STAGE " + stage;
            Destroy(newStageClone, 5.0f);
        }
    }
}