using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour {
    public enum GameState {
        MainMenu,
        Playing,
        GameOver,
        PreparingUI
    }

    public GameObject arrowObject;
    public GameObject gameBackgroundObject;
    public GameState gameState;

    public Text gameTimeText;
    public GameObject mainMenuGameObject;
    private float menuElementsSpeed = 0.03f;

    public GameObject playerGameObject;
    private float score;
    private int stage;
    private float time;
    // Use this for initialization

    private void Start() {
        ColorPalette.InitPalleteNum(1);
        Camera.main.backgroundColor = ColorPalette.background1Color;
        gameBackgroundObject.GetComponent<SpriteRenderer>().color = ColorPalette.background2Color;
        mainMenuGameObject.GetComponent<SpriteRenderer>().color = ColorPalette.background2Color;
        arrowObject.GetComponent<SpriteRenderer>().color = ColorPalette.background1Color;
        playerGameObject.GetComponent<SpriteRenderer>().color = ColorPalette.playerColor;

        gameState = GameState.MainMenu;
        mainMenuGameObject.transform.position = new Vector3(0, 0, 1);
        gameBackgroundObject.transform.position = new Vector3(0,
            2*Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y, 0);

        score = 0;
        time = 0;
        stage = 1;
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

}