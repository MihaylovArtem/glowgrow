using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    private readonly Stack<GameObject> stackOfCatchBullets = new Stack<GameObject>();
    private int MaxSize;
    private Vector3 addScale = new Vector3(0.05f, 0.05f, 0.05f);

    private Vector3 addZposition;
    public Color currentGlowColor;
    public GameObject glowGameObject;
    private SpriteRenderer glowSpriteRenderer;
    private Vector3 nextRowScale;
    public GameObject playerGameObject;
    public GameObject rowGameObject;
    // Use this for initialization
    private void Start() {
        glowSpriteRenderer = glowGameObject.GetComponent<SpriteRenderer>();
        nextRowScale = playerGameObject.transform.localScale;
        addZposition = new Vector3(0, 0, 0.01f);
    }

    private void ChangePlayerColor() {
        ChangeColorFromTo(glowSpriteRenderer.color, currentGlowColor);

        if (Input.GetKeyUp(KeyCode.LeftArrow)) {
            currentGlowColor = ColorPalette.bullet1Color;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow)) {
            currentGlowColor = ColorPalette.bullet2Color;
        }
    }

    private void ChangeColorFromTo(Color col1, Color col2) {
        float t = Time.deltaTime*5;
        glowSpriteRenderer.color = Color.Lerp(col1, col2, t);
    }

    private void RotateGlow() {
        glowGameObject.transform.Rotate(0, 0, 1);
    }

    // Update is called once per frame
    private void Update() {
        RotateGlow();
        ChangePlayerColor();
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Bullet") {
            if (col.gameObject.GetComponent<SpriteRenderer>().color == currentGlowColor) {
                var catchedBullet =
                    (GameObject)
                        Instantiate(rowGameObject, playerGameObject.transform.position + addZposition,
                            Quaternion.identity);
                addZposition += new Vector3(0, 0, 0.01f);
                nextRowScale += addScale;
                catchedBullet.transform.localScale = nextRowScale;
                glowGameObject.transform.localScale = nextRowScale;
                catchedBullet.GetComponent<SpriteRenderer>().color = col.GetComponent<SpriteRenderer>().color;
                playerGameObject.GetComponent<CircleCollider2D>().radius *= (1 + addScale.x);
                catchedBullet.GetComponent<SpriteRenderer>().color = col.gameObject.GetComponent<SpriteRenderer>().color;
                stackOfCatchBullets.Push(catchedBullet);
                Debug.Log(stackOfCatchBullets.Peek().GetComponent<SpriteRenderer>().color);
            }
            else {
                //все объекты одного цвета разлетаются на куски
                Color destroyedColor = stackOfCatchBullets.Peek().GetComponent<SpriteRenderer>().color;
                bool destroy = true;
                while (stackOfCatchBullets.Count > 0 && destroy) {
                    if (stackOfCatchBullets.Peek().GetComponent<SpriteRenderer>().color == destroyedColor)
                    {
                        nextRowScale -= addScale;
                        glowGameObject.transform.localScale = nextRowScale;
                        GameObject rowToDestroy = stackOfCatchBullets.Pop();
                        Destroy(rowToDestroy);
                        playerGameObject.GetComponent<CircleCollider2D>().radius /= (1 + addScale.x);
                    }
                    else {
                        destroy = false;
                    }
                }
            }
            Destroy(col.gameObject);
        }
    }
}