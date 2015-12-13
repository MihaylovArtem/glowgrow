using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


    public GameObject playerGameObject;
    public GameObject glowGameObject;
    public GameObject rowGameObject;
    private SpriteRenderer glowSpriteRenderer;
    public Color currentGlowColor;
    private Stack<GameObject> stackOfCatchBullets = new Stack<GameObject>();
    private int MaxSize;
    private Vector3 addScale = new Vector3(0.05f, 0.05f, 0.05f);

    private Vector3 nextRowScale;
    private Vector3 addZposition;
	// Use this for initialization
    void Start() {
        glowSpriteRenderer = glowGameObject.GetComponent<SpriteRenderer>();
	    nextRowScale = playerGameObject.transform.localScale +addScale;
	    addZposition = new Vector3(0, 0, 0.01f);
	}

    void ChangePlayerColor()
    {
        ChangeColorFromTo(glowSpriteRenderer.color, currentGlowColor);

            if (Input.GetKeyUp(KeyCode.LeftArrow)) {
                currentGlowColor = ColorPalette.bullet1Color;
            }

            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                currentGlowColor = ColorPalette.bullet2Color;
            }
        
    }

    void ChangeColorFromTo(Color col1, Color col2) {
        float t = Time.deltaTime*5;
        glowSpriteRenderer.color = Color.Lerp(col1, col2, t);
    }

    void RotateGlow() {
        glowGameObject.transform.Rotate(0,0,1);
    }

	// Update is called once per frame
	void Update () {
	    RotateGlow();
	    ChangePlayerColor();
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            if (col.gameObject.GetComponent<SpriteRenderer>().color ==
                currentGlowColor)
            {
                var catchedBullet = (GameObject) Instantiate(rowGameObject, playerGameObject.transform.position + addZposition, Quaternion.identity);
                addZposition += new Vector3(0, 0, 0.01f);
                catchedBullet.transform.localScale = nextRowScale;
                catchedBullet.GetComponent<SpriteRenderer>().color = col.GetComponent<SpriteRenderer>().color;
                glowGameObject.transform.localScale = nextRowScale;
                nextRowScale += addScale;
                catchedBullet.GetComponent<SpriteRenderer>().color = col.gameObject.GetComponent<SpriteRenderer>().color;
                stackOfCatchBullets.Push(catchedBullet);
                Destroy(col.gameObject);
            }
        }
    }

}
