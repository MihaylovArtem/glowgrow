using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    private Color bulletColor;
    public int bulletType = 1;
    private float bulletSpeed;

    public string bulletMoveType;
    



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


    private void OnTriggerStay2D(Collider2D col)
    {
        var player = col.gameObject.GetComponent<Player>();
        if (col.gameObject.tag == "Player")
        {
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
                    while (destroy && player.stackOfCatchBullets.Count > 0)
                    {
                        if (player.stackOfCatchBullets.Peek().GetComponent<SpriteRenderer>().color == destroyedColor)
                        {
                            player.DestroyTopRow();
                        }
                        else
                        {
                            destroy = false;
                        }
                    }
                }
                else
                {
                    player.DestroySelf();
                }
            }
            Destroy(gameObject);
        }
    }

	// Update is called once per frame
	void Update () {
	    if (bulletMoveType == "spiral")
	    {
	        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2 (-gameObject.transform.position.x, -gameObject.transform.position.y));
	    }
	}
}
