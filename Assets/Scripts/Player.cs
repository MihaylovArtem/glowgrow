using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {


    public GameObject playerGameObject;
    public GameObject glowGameObject;
    private Stack<Bullet> stackOfCatchBullets = new Stack<Bullet>();
    private SpriteRenderer glowSpriteRenderer;
    public Color currentGlowColor;
    private int MaxSize;
	// Use this for initialization
    void Start() {
        glowSpriteRenderer = glowGameObject.GetComponent<SpriteRenderer>();
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
        glowGameObject.transform.Rotate(0,0,Time.deltaTime);
    }

	// Update is called once per frame
	void Update () {
	    //RotateGlow();
	    ChangePlayerColor();
	}
}
