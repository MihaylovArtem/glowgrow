using Microsoft.Win32;
using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public GameObject bulletGameObject;
 
    private Color bulletColor;
    public int bulletType;
    private float bulletSpeed;

    private int k = 0;


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
        //bulletColor = <берем из палитры >;
	    //int k = 0;
	}
	
	// Update is called once per frame

    void SpawnSingleBullet()
    {
        float radius = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float angle = Mathf.PI* 2* Random.Range(0.0f, 1.0f);
        float x = Mathf.Sin(angle)*radius;
        float y = Mathf.Cos(angle)*radius;
        var createdBullet = (GameObject)Instantiate(bulletGameObject, new Vector3(x, y, 0),Quaternion.identity);
        createdBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50*x,-50*y));
        //return createdBullet;
    }
	void Update ()
	{
	    if (k < 5)
	    {
	        k++;
	        SpawnSingleBullet();
	    }
	}
}
