using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
    
    private ColorPalette.Colors bulletColor = ColorPalette.Colors.Green;
    private float bulletSpeed;
    //private BUlletType square; - элемент enum класса типов
 

	// Use this for initialization
	void Start ()
	{
	    //bulletSpeed = Manager.Time*0.01;
        //bulletColor = <берем из палитры >;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
