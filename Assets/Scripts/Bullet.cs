using System;
using UnityEngine;
using System.Collections;
using UnityEngine.SocialPlatforms.Impl;

public class Bullet : MonoBehaviour {

    private Color bulletColor;
    public int bulletType = 1;
    public float bulletSpeed;
    public float bulletCurve;

    public string bulletMoveType;
    



    // Use this for initialization
    void Start()
    {
      
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


    private void OnTriggerStay2D(Collider2D col)
    {
        var player = col.gameObject.GetComponent<Player>();
        if (col.gameObject.tag == "Player")
        {
            Manager.totalScore += 10;
            if (Manager.totalScore > Manager.recordScore)
                Manager.recordScore = Manager.totalScore;
            if (gameObject.GetComponent<SpriteRenderer>().color == player.currentGlowColor)
            {
                player.CreateRowWithBullet(gameObject);
            }
            else
            {
                if (player.stackOfCatchBullets.Count > 0)
                {
                    //все объекты одного цвета разлетаются на куски
                    Color destroyedColor = player.stackOfCatchBullets.Peek().GetComponent<SpriteRenderer>().color;
                    bool destroy = true;
                    player.isPopOccured = true;
                    int numberOfDestroyedRows = 0;
                    while (destroy && player.stackOfCatchBullets.Count > 0)
                    {
                        if (player.stackOfCatchBullets.Peek().GetComponent<SpriteRenderer>().color == destroyedColor)
                        {
                            numberOfDestroyedRows++;
                            player.DestroyTopRow();
                        }
                        else
                        {
                            destroy = false;
                        }
                    }
                    Debug.Log(numberOfDestroyedRows);
                    Manager.totalScore += Convert.ToInt32(numberOfDestroyedRows*10*Manager.multiplayer);
                    if (Manager.totalScore > Manager.recordScore)
                        Manager.recordScore = Manager.totalScore;
                }
                else
                {
                    player.DestroySelf();
                }
            }
//            if (GetComponent<SpriteRenderer>().color==ColorPalette.bullet1Color) {
//                sourceClone.GetComponent<AudioSource>().clip = sound1;
//            }
//            if (GetComponent<SpriteRenderer>().color == ColorPalette.bullet2Color)
//            {
//                sourceClone.GetComponent<AudioSource>().clip = sound2;
//            }
//            sourceClone.GetComponent<AudioSource>().Play();
//            Destroy(sourceClone,5.0f);
            Destroy(gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
	    if (bulletMoveType == "spiral") {
            Vector2 vect = new Vector2(gameObject.transform.position.y * bulletSpeed, -gameObject.transform.position.x * bulletSpeed) - new Vector2(gameObject.transform.position.x, gameObject.transform.position.y).normalized * bulletCurve*bulletSpeed;
	        gameObject.GetComponent<Rigidbody2D>().velocity = vect;
	    }
	}
}
