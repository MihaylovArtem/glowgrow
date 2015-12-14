using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Pattern : MonoBehaviour {
    public GameObject bulletGameObject;

    public float force;
    public GameObject pattern;
    public GameObject tutorialTextPrefab;
    public GameObject tutorialTextPrefab2;
    public int patternNumber;
    // Use this for initialization
    public IEnumerator InitPatternWithNum(int patNum, float waitTime) {
        yield return new WaitForSeconds(waitTime);
        force = 25f;
        Debug.Log(patNum);
        if (patNum == 1) {
            for (int i = 1; i <= Manager.stage*6; i++) {
                StartCoroutine(CreateBullets(4, i/6f, i%4, i));
            }
            if (patNum == 2)
            {
                CreateSpiralBullets(1, 0f, 1, 1);
            }
        
        
}
    }

    public void StartTutorial(bool again) {
        if (again) {
            StopCoroutine("CreateTutorialText");
            StopCoroutine("CreateBullets");
            StartCoroutine(CreateTutorialText("Let's try again",0.0f,1));
            StartCoroutine(CreateTutorialText("Match your color with bullet's", 3.0f, 2));
            StartCoroutine(CreateBullets(1, 0.0f, 1, 3));
            StartCoroutine(CreateTutorialText("Press arrows to change your color", 6.0f, 1));
            StartCoroutine(CreateBullets(1, 0.5f, 2, 8));
            StartCoroutine(CreateTutorialText("The rows of same color will explode if you catch the wrong particle", 10.0f, 2));
            StartCoroutine(CreateTutorialText("Good luck!", 13.0f, 1));
            StartCoroutine(InitPatternWithNum(1, 14.0f));

        }
        else {
            StartCoroutine(CreateTutorialText("Let's learn how to play this game", 0.0f,1));
            StartCoroutine(CreateTutorialText("It's very simple", 3.0f,2));
            StartCoroutine(CreateTutorialText("Match your color with bullet's", 6.0f,1));
            StartCoroutine(CreateBullets(1, 0.0f, 1, 6));
            StartCoroutine(CreateTutorialText("Press arrows to change your color", 9.0f,1));
            StartCoroutine(CreateBullets(1, 0.5f, 2, 11));
            StartCoroutine(CreateTutorialText("The row will explode if you catch the wrong particle", 13.0f, 2));
            StartCoroutine(CreateTutorialText("Good luck!", 16.0f, 1));
            StartCoroutine(InitPatternWithNum(1, 17.0f));
            PlayerPrefs.SetInt("TutorialCompleted", 1);
        }
    }

    public IEnumerator CreateTutorialText(string text, float waitTime, int prefabType) {
        yield return new WaitForSeconds(waitTime);
        if (text == "Good luck!") {
            PlayerPrefs.SetInt("TutorialCompleted", 1);
        }
        GameObject tutorialTextPrefabClone;
        GameObject canvas = GameObject.Find("Canvas");
        if (prefabType == 1) {
            tutorialTextPrefabClone = Instantiate(tutorialTextPrefab);

        }
        else
        {
            tutorialTextPrefabClone = Instantiate(tutorialTextPrefab2);
            
        }
        tutorialTextPrefabClone.transform.SetParent(canvas.transform, false);
        tutorialTextPrefabClone.GetComponent<Text>().text = text;
        Destroy(tutorialTextPrefabClone, 8f);
    }

    public GameObject SpawnSingleBullet(float choosedAngle) {
        float radius = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float angle = Mathf.PI*2*choosedAngle;
        float y = Mathf.Sin(angle)*radius;
        float x = Mathf.Cos(angle)*radius;
        var createdBullet = (GameObject) Instantiate(bulletGameObject, new Vector3(x, y, 0.01f), Quaternion.identity);
        int color = Random.Range(1, 3);
        return createdBullet;
    }

    private IEnumerator CreateBullets(int num, float pos, int color, float delayTime) {
        yield return new WaitForSeconds(delayTime);
        Spawner.objectWithPatternCreated += num;
        for (int i = 0; i < num; i++) {
            float position = pos + 1.0f*i/num;
            if (position >= 1.0f) {
                position -= 1.0f;
            }
            if (Manager.gameState == Manager.GameState.Playing || Manager.gameState == Manager.GameState.Tutorial)
            {
                GameObject createdBullet = SpawnSingleBullet(position);
                createdBullet.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(force*(-createdBullet.transform.position.x), force*(-createdBullet.transform.position.y)));
                if (color == 1)
                {
                    createdBullet.GetComponent<SpriteRenderer>().color = ColorPalette.bullet1Color;
                }
                else
                {
                    createdBullet.GetComponent<SpriteRenderer>().color = ColorPalette.bullet2Color;
                }
            }
        }
    }
    private IEnumerator CreateSpiralBullets(int num, float pos, int color, float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Spawner.objectWithPatternCreated += num;
        for (int i = 0; i < num; i++)
        {
            float position = pos + 1.0f * i / num;
            if (position >= 1.0f)
            {
                position -= 1.0f;
            }
            if (Manager.gameState == Manager.GameState.Playing || Manager.gameState == Manager.GameState.Tutorial)
            {
                GameObject createdBullet = SpawnSingleBullet(position);
                createdBullet.GetComponent<Bullet>().bulletMoveType = "spiral";
                createdBullet.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(force * (createdBullet.transform.position.y)/(createdBullet.transform.position.x), force * (-createdBullet.transform.position.y)));
                if (color == 1)
                {
                    createdBullet.GetComponent<SpriteRenderer>().color = ColorPalette.bullet1Color;
                }
                else
                {
                    createdBullet.GetComponent<SpriteRenderer>().color = ColorPalette.bullet2Color;
                }
            }
        }
    }
}