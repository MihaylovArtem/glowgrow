using System.Collections;
using UnityEngine;

public class Pattern : MonoBehaviour {
    public GameObject bulletGameObject;

    public float force;
    public GameObject pattern;
    public int patternNumber;
    // Use this for initialization
    public IEnumerator InitPatternWithNum(int patNum, float waitTime) {
        yield return new WaitForSeconds(waitTime);
        force = 25f;
        Debug.Log(patNum);
        if (patNum == 0)
        {
            Debug.Log(patNum);
            StartCoroutine(CreateBullets(1, 0.25f, 1, 2));
            StartCoroutine(CreateBullets(1, 0.75f, 2, 6));
            StartCoroutine(InitPatternWithNum(1, 10));
        }
        if (patNum == 1) {
            for (int i = 1; i <= Manager.stage*16; i++) {
                StartCoroutine(CreateBullets(4, i/16.0f, i%4, i));
            }
        }


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
            if (Manager.gameState == Manager.GameState.Playing)
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
}