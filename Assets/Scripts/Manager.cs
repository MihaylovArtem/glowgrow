using System;
using System.Runtime.Remoting.Messaging;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    private float score;
    private float time;
    private int stage;
    private int isInGame = 0;
    private int isInMainMenu = 0;
    private int isInHighscores = 0;
    private int isInSettings = 0;
    private int isInExit = 0;

    private ColorPalette.Colors stagePalette;

    private float menuElementsSpeed = 0.3f;

    public GameObject playerGameObject;
    public GameObject MainMenuGameObject;
    public GameObject SettingsGameObject;
    public GameObject ExitGameObject;
    public GameObject BackGroundGameObject;
    public GameObject HighscoresGameObject;

    public Text gameTimeText;
    // Use this for initialization
    void Start()
    {

        MainMenuGameObject.transform.position = new Vector3(0, 0, 1);
        BackGroundGameObject.transform.position = new Vector3(0,
            2*Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y, 0);
        SettingsGameObject.transform.position =
            new Vector3(2*Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x, 0, 0);
        ExitGameObject.transform.position = new Vector3(0,
            -2*Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y, 0);
        HighscoresGameObject.transform.position =
            new Vector3(-2*Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x, 0, 0);



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
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.G) && isInGame == 0)
        {
            isInGame = 1;
        }
        if (Input.GetKeyUp(KeyCode.Escape) && isInGame == 2)
        {
            isInGame = -1;
        }

        if (Input.GetKeyUp(KeyCode.H) && isInSettings == 0)
        {
            isInSettings = 1;
        }
        if (Input.GetKeyUp(KeyCode.Escape) && isInSettings == 2)
        {
            isInSettings = -1;
        }

        if (Input.GetKeyUp(KeyCode.R) && isInHighscores == 0)
        {
            isInHighscores = 1;
        }
        if (Input.GetKeyUp(KeyCode.Escape) && isInHighscores == 2)
        {
            isInHighscores = -1;
        }

        if (Input.GetKeyUp(KeyCode.Z) && isInExit == 0)
        {
            isInExit = 1;
        }
        if (isInGame == 1)
        {
            if (BackGroundGameObject.transform.position.y > 0)
            {
                BackGroundGameObject.transform.position -= new Vector3(0, menuElementsSpeed, 0);
            }
            else
            {
                isInGame = 2;
            }
        }
        if (isInSettings == 1)
        {
            if (SettingsGameObject.transform.position.x > 0)
            {
                SettingsGameObject.transform.position -= new Vector3(menuElementsSpeed, 0, 0);
            }
            else
            {
                isInSettings = 2;
            }
        }
        if (isInHighscores == 1)
        {
            if (HighscoresGameObject.transform.position.x < 0)
            {
                HighscoresGameObject.transform.position += new Vector3(menuElementsSpeed, 0, 0);
            }
            else
            {
                isInHighscores = 2;
            }
        }
        if (isInExit == 1)
        {
            if (ExitGameObject.transform.position.y < 0)
            {
                ExitGameObject.transform.position += new Vector3(0, menuElementsSpeed, 0);
            }
            else
            {
                Application.Quit();
            }
        }
        if (isInGame == -1)
        {
            if (BackGroundGameObject.transform.position.y < 2*Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y)
            {
                BackGroundGameObject.transform.position += new Vector3(0, menuElementsSpeed, 0);
            }
            else
            {
                isInGame = 0;
            }
        }
        if (isInSettings == -1)
        {
            if (SettingsGameObject.transform.position.x < 2 * Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x)
            {
                SettingsGameObject.transform.position += new Vector3(menuElementsSpeed, 0, 0);
            }
            else
            {
                isInSettings = 0;
            }
        }
        if (isInHighscores == -1)
        {
            if (HighscoresGameObject.transform.position.x > -2 * Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0)).x)
            {
                HighscoresGameObject.transform.position -= new Vector3(menuElementsSpeed, 0, 0);
            }
            else
            {
                isInHighscores = 2;
            }
        }
    }
}

