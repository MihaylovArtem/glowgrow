﻿using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private Color bulletColor;
    public int bulletType = 1;
    private float bulletSpeed;



    // Use this for initialization
    void Start()
    {
        //bulletSpeed = Manager.Time*0.01;
      
    }

    public void SetBulletColor()
    {
        if (bulletType == 1)
        {
            bulletColor = ColorPalette.bullet1Color;
        }

        if (bulletType == 2)
        {
            bulletColor = ColorPalette.bullet2Color;
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
