using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    public GameObject playerGameObject;
    private Stack<Bullet> stackOfCatchBullets = new Stack<Bullet>();
    //private ColorPalette.PlayerColor = //<current Color off the Player>;
    private int MaxSize;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
