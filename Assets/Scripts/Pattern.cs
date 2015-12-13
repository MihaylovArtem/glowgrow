using UnityEngine;
using System.Collections;

public class Pattern : MonoBehaviour {

    public int patternNumber;

    public GameObject bulletGameObject;
    public GameObject pattern;

    public float force;
	// Use this for initialization
	void Start ()
	{
	    force = 50f;
	}
    public GameObject SpawnSingleBullet(float choosedAngle)
    {
        float radius = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0)).y;
        float angle = Mathf.PI * 2 * choosedAngle;
        float y = Mathf.Sin(angle) * radius;
        float x = Mathf.Cos(angle) * radius;
        var createdBullet = (GameObject)Instantiate(bulletGameObject, new Vector3(x, y, 0.01f), Quaternion.identity);
        int color = Random.Range(1, 3);
        return createdBullet;
    }
	// Update is called once per frame
	void Update () {

	    if (patternNumber == 0)
	    {
            if (Spawner.curTime >= 5f && Spawner.objectWithPatternCreated == 0)
            {
                Spawner.objectWithPatternCreated += 1;
                GameObject createdBullet = SpawnSingleBullet(0.5f);
                createdBullet.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(force * (-createdBullet.transform.position.x), 0));
                createdBullet.GetComponent<SpriteRenderer>().color = ColorPalette.bullet1Color;
            }
            if (Spawner.curTime >= 10f && Spawner.objectWithPatternCreated == 1)
            {
                Spawner.objectWithPatternCreated += 1;
                GameObject createdBullet = SpawnSingleBullet(0f);
                createdBullet.GetComponent<Rigidbody2D>().AddForce(
                   new Vector2(force * (-createdBullet.transform.position.x), 0));
                createdBullet.GetComponent<SpriteRenderer>().color = ColorPalette.bullet2Color;
            }
	        if (Spawner.curTime >= 15f)
	        {
	            Spawner.curTime = 0;
	            Spawner.objectWithPatternCreated = 0;
	            var newPattern = (GameObject)Instantiate(pattern, Vector3.zero, Quaternion.identity);
	            newPattern.GetComponent<Pattern>().patternNumber = 1;
                Destroy(this);
	        }


	    }
	    if (patternNumber == 1)
	    {
            if (Spawner.curTime >= 0f && Spawner.objectWithPatternCreated == 0)
            {
                Spawner.objectWithPatternCreated += 2;
                GameObject createdBullet1 = SpawnSingleBullet(0.25f);
                createdBullet1.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(force * (-createdBullet1.transform.position.x),  force * (-createdBullet1.transform.position.y)));
                createdBullet1.GetComponent<SpriteRenderer>().color = ColorPalette.bullet1Color;
                GameObject createdBullet2 = SpawnSingleBullet(0.75f);
                createdBullet2.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(force * (-createdBullet2.transform.position.x), force * (-createdBullet2.transform.position.y)));
                createdBullet2.GetComponent<SpriteRenderer>().color = ColorPalette.bullet1Color;
            }
            if (Spawner.curTime >= 0.5f && Spawner.objectWithPatternCreated == 2)
            {
                Spawner.objectWithPatternCreated += 2;
                GameObject createdBullet1 = SpawnSingleBullet(0.125f);
                createdBullet1.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(force * (-createdBullet1.transform.position.x), force * (-createdBullet1.transform.position.y)));
                createdBullet1.GetComponent<SpriteRenderer>().color = ColorPalette.bullet2Color;
                GameObject createdBullet2 = SpawnSingleBullet(0.625f);
                createdBullet2.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(force * (-createdBullet2.transform.position.x), force * (-createdBullet2.transform.position.y)));
                createdBullet2.GetComponent<SpriteRenderer>().color = ColorPalette.bullet2Color;
            }
            if (Spawner.curTime >= 1f && Spawner.objectWithPatternCreated == 4)
            {
                Spawner.objectWithPatternCreated += 2;
                GameObject createdBullet1 = SpawnSingleBullet(0f);
                createdBullet1.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(force * (-createdBullet1.transform.position.x), force * (-createdBullet1.transform.position.y)));
                createdBullet1.GetComponent<SpriteRenderer>().color = ColorPalette.bullet1Color;
                GameObject createdBullet2 = SpawnSingleBullet(0.5f);
                createdBullet2.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(force * (-createdBullet2.transform.position.x), force * (-createdBullet2.transform.position.y)));
                createdBullet2.GetComponent<SpriteRenderer>().color = ColorPalette.bullet1Color;
            }
            if (Spawner.curTime >= 1.5f && Spawner.objectWithPatternCreated == 6)
            {
                Spawner.objectWithPatternCreated += 2;
                GameObject createdBullet1 = SpawnSingleBullet(0.875f);
                createdBullet1.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(force * (-createdBullet1.transform.position.x), force * (-createdBullet1.transform.position.y)));
                createdBullet1.GetComponent<SpriteRenderer>().color = ColorPalette.bullet2Color;
                GameObject createdBullet2 = SpawnSingleBullet(0.375f);
                createdBullet2.GetComponent<Rigidbody2D>().AddForce(
                    new Vector2(force * (-createdBullet2.transform.position.x), force * (-createdBullet2.transform.position.y)));
                createdBullet2.GetComponent<SpriteRenderer>().color = ColorPalette.bullet2Color;
            }
            
            if (Spawner.curTime >= 2f)
            {
                Spawner.curTime = 0;
                Spawner.objectWithPatternCreated = 0;
                //var newPattern = (Pattern)Instantiate(pattern, Vector3.zero, Quaternion.identity);
                //newPattern.GetComponent<Pattern>().patternNumber = 1;
                Destroy(this);
            }
	    }
	    if (patternNumber == 2)
	    {
	        
	    }
	}
}
