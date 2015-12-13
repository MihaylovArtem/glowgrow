using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    

    public GameObject playerGameObject;
    private Stack<Bullet> stackOfCatchBullets = new Stack<Bullet>();
    private int MaxSize;
	// Use this for initialization
	void Start () {
        this.GetComponent<SpriteRenderer>().color = ColorPalette.playerColor;
	}

    void ChangePlayerColor()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            playerGameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            playerGameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
	// Update is called once per frame
	void Update () 
    {
	    ChangePlayerColor();
	}
}
