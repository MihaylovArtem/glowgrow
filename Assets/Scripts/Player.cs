using System.Collections.Generic;
using System.Runtime.InteropServices;
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
	
	// Update is called once per frame
	void Update () {
	    
	}
}
