using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private readonly Vector3 addScale = new Vector3(0.05f, 0.05f, 0.05f);
    public readonly Stack<GameObject> stackOfCatchBullets = new Stack<GameObject>();
    private int MaxSize;


    private Vector3 addZposition;
    public Color currentGlowColor;
    public float currentSize;
    public GameObject explosion;
    private float firstColliderSize = 0.64f;
    public GameObject glowGameObject;
    private SpriteRenderer glowSpriteRenderer;
    public bool isPopOccured;
    private Vector3 nextRowScale;
    public GameObject playerGameObject;
    public GameObject rowGameObject;
    private GameObject topRow;
    // Use this for initialization
    private void Start() {
        glowSpriteRenderer = glowGameObject.GetComponent<SpriteRenderer>();
        nextRowScale = playerGameObject.transform.localScale;
        currentSize = nextRowScale.x;
        addZposition = new Vector3(0, 0, 0.01f);
    }

    private void ChangePlayerColor() {
        ChangeColorFromTo(glowSpriteRenderer.color, currentGlowColor);

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            currentGlowColor = ColorPalette.bullet1Color;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            currentGlowColor = ColorPalette.bullet2Color;
        }
    }

    private void ChangeColorFromTo(Color col1, Color col2) {
        float t = Time.deltaTime*5;
        col2 = new Color(col2.r, col2.g, col2.b, 0.5f);
        glowSpriteRenderer.color = Color.Lerp(col1, col2, t);
    }

    private void ChangeSizeFromTo(float size1, float size2) {
        float t = Time.deltaTime*5;
        float lerp = Mathf.Lerp(size1, size2, t);
        glowGameObject.transform.localScale = new Vector3(lerp, lerp, 1);
        if (stackOfCatchBullets.Count > 0) {
            if (!isPopOccured) {
                stackOfCatchBullets.Peek().transform.localScale = new Vector3(lerp, lerp, 1);
            }
        }
    }


    private void RotateGlow() {
        glowGameObject.transform.Rotate(0, 0, 1);
    }

    // Update is called once per frame
    private void Update() {
        RotateGlow();
        ChangePlayerColor();
        ChangeSizeFromTo(glowGameObject.transform.localScale.x, currentSize);
        CheckIfStageEnded();
    }


    public void DestroyTopRow() {
        //create explosion
        if (stackOfCatchBullets.Count > 0) {
            nextRowScale -= addScale;
            currentSize = nextRowScale.x;
            GameObject rowToDestroy = stackOfCatchBullets.Pop();

            var expl = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
            playerGameObject.GetComponent<CircleCollider2D>().radius = firstColliderSize*nextRowScale.x;
            expl.GetComponent<ParticleSystem>().startColor = rowToDestroy.GetComponent<SpriteRenderer>().color;
            expl.GetComponent<ParticleSystem>().Emit(15);
            Destroy(expl, 5f);

            Destroy(rowToDestroy);
        }
        else {
            CancelInvoke("DestroyTopRow");
        }
    }

    public void CreateRowWithBullet(GameObject bullet) {
        isPopOccured = false;
        var catchedBullet =
            (GameObject)
                Instantiate(rowGameObject, playerGameObject.transform.position + addZposition,
                    Quaternion.identity);
        addZposition += new Vector3(0, 0, 0.01f);
        nextRowScale += addScale;
        currentSize = nextRowScale.x;
        playerGameObject.GetComponent<CircleCollider2D>().radius = firstColliderSize*nextRowScale.x;
        catchedBullet.GetComponent<SpriteRenderer>().color = bullet.GetComponent<SpriteRenderer>().color;
        catchedBullet.GetComponent<SpriteRenderer>().color = bullet.gameObject.GetComponent<SpriteRenderer>().color;
        stackOfCatchBullets.Push(catchedBullet);
    }

    private void CheckIfStageEnded() {
        if (stackOfCatchBullets.Count >= 40) {
            GameObject.Find("Main Camera").GetComponent<Manager>().EndStage();
            InvokeRepeating("DestroyTopRow", 3f, 0.001f);
        }
    }


    public void DestroySelf() {
        foreach (GameObject bullet in GameObject.FindGameObjectsWithTag("Bullet")) {
            var bulletExpl = Instantiate(explosion, bullet.transform.position, bullet.transform.rotation) as GameObject;
            bulletExpl.GetComponent<ParticleSystem>().startColor = bullet.GetComponent<SpriteRenderer>().color;
            bulletExpl.GetComponent<ParticleSystem>().Emit(15);
            Destroy(bulletExpl, 5f);
            Destroy(bullet);
        }
        Manager.gameState = Manager.GameState.PreparingNewStage;
        Invoke("DestroyPlayer", 2.0f);
    }

    public void DestroyPlayer() {
        var expl = Instantiate(explosion, transform.position, transform.rotation) as GameObject;
        expl.GetComponent<ParticleSystem>().startColor = gameObject.GetComponent<SpriteRenderer>().color;
        expl.GetComponent<ParticleSystem>().Emit(120);

        Destroy(expl, 5f);

        Destroy(gameObject);
    }
}