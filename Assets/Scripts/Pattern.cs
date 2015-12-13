using System.Collections;
using UnityEngine;

public class Pattern : MonoBehaviour {
    public GameObject bulletGameObject;

    public float force;
    public GameObject pattern;
    public int patternNumber;
    // Use this for initialization
    private void Start() {
        force = 25f;
        GameObject.Find("Main Camera").GetComponent<Manager>().BeginNextStage();
        if (patternNumber == 1)
        {
            for (int i = 1; i <= Manager.stage * 16; i++)
            {
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

    // Update is called once per frame
    private void Update() {
        if (patternNumber == 0) {
            if (Spawner.curTime >= 5f && Spawner.objectWithPatternCreated == 0) {
                Spawner.objectWithPatternCreated += 1;
                GameObject createdBullet = SpawnSingleBullet(0.5f);
                createdBullet.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(force*(-createdBullet.transform.position.x), 0));
                createdBullet.GetComponent<SpriteRenderer>().color = ColorPalette.bullet1Color;
            }
            if (Spawner.curTime >= 10f && Spawner.objectWithPatternCreated == 1) {
                Spawner.objectWithPatternCreated += 1;
                GameObject createdBullet = SpawnSingleBullet(0f);
                createdBullet.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(force*(-createdBullet.transform.position.x), 0));
                createdBullet.GetComponent<SpriteRenderer>().color = ColorPalette.bullet2Color;
            }
            if (Spawner.curTime >= 15f) {
                Spawner.curTime = 0;
                Spawner.objectWithPatternCreated = 0;
                var newPattern = (GameObject) Instantiate(pattern, Vector3.zero, Quaternion.identity);
                newPattern.GetComponent<Pattern>().patternNumber = 1;
                Destroy(this);
            }
        }
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