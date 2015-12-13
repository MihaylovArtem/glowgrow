using Microsoft.Win32;
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
    public GameObject bulletGameObject;
 

	// Use this for initialization
	void Start ()
	{
	    InvokeRepeating("SpawnSingleBullet", 2f, 1f);
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
        int color = Random.Range(1, 3);
        if (color == 1) {
            createdBullet.GetComponent<SpriteRenderer>().color = ColorPalette.bullet1Color;

        }
        else {

            createdBullet.GetComponent<SpriteRenderer>().color = ColorPalette.bullet2Color;
        }
        createdBullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(-50*x,-50*y));
        //return createdBullet;
    }
	void Update ()
	{
	}
}
