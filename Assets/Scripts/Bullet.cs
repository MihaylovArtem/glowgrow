using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    
    private Color bulletColor;
    public int bulletType;
    private float bulletSpeed;
    //private BUlletType square; - элемент enum класса типов
 

	// Use this for initialization
	void Start ()
	{
	    //bulletSpeed = Manager.Time*0.01;
	    if (bulletType == 1) {
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
