using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEditor.VersionControl;
using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    

    public GameObject playerGameObject;
    public GameObject bulletGameObject;
    private Stack<GameObject> stackOfCatchBullets = new Stack<GameObject>();
    private int MaxSize;
    private Vector3 addScale = new Vector3(0.05f, 0.05f, 0.05f);

    private Vector3 nextRowScale;
    private Vector3 addZposition;
	// Use this for initialization
	void Start ()
	{
	    nextRowScale = playerGameObject.transform.localScale +addScale;
	    addZposition = new Vector3(0, 0, 0.01f);
	}

    void ChangePlayerColor()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            playerGameObject.GetComponent<SpriteRenderer>().color = Color.red;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            playerGameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
	// Update is called once per frame
	void Update () 
    {
	    ChangePlayerColor();
	}

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            if (col.gameObject.GetComponent<SpriteRenderer>().color ==
                playerGameObject.GetComponent<SpriteRenderer>().color)
            {
                var catchedBullet =
                    (GameObject)
                        Instantiate(playerGameObject, playerGameObject.transform.position + addZposition,
                            Quaternion.identity);
                addZposition += new Vector3(0, 0, 0.01f);
                catchedBullet.transform.localScale = nextRowScale;
                nextRowScale += addScale;
                playerGameObject.GetComponent<CircleCollider2D>().radius *= (1+addScale.x);
                catchedBullet.GetComponent<SpriteRenderer>().color = col.gameObject.GetComponent<SpriteRenderer>().color;
                stackOfCatchBullets.Push(catchedBullet);
            }
            else
            {
                //все объекты одного цвета разлетаются на куски
                Color destroyedColor = stackOfCatchBullets.Pop().GetComponent<SpriteRenderer>().color;
                while (stackOfCatchBullets.Peek().GetComponent<SpriteRenderer>().color == destroyedColor)
                {
                    stackOfCatchBullets.Pop();
                    //объект разлетается на куски;
                    playerGameObject.GetComponent<CircleCollider2D>().radius *= (1 + addScale.x);


                    
                }
            }
            Destroy(col.gameObject);
        }
    }

}
