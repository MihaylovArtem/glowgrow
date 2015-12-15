using UnityEngine;
using System.Collections;

public class ColorPalette : MonoBehaviour {
    static public Color playerColor;
    static public Color bullet1Color;
    static public Color bullet2Color;
    static public Color background1Color;
    static public Color background2Color;
    
    // Use this for initialization
	public static void InitPalleteNum (int i) {
        switch (i)
        {
            case 0:
                {
                    playerColor = new Color(0.5f, 0.5f, 0.5f);
                    bullet1Color = new Color(0.5f, 0.5f, 0.5f);
                    bullet2Color = new Color(0.5f, 0.5f, 0.5f);
                    background1Color = new Color(0.7f, 0.7f, 0.7f);
                    background2Color = playerColor;
                    break;
                }
            case 1: {
                playerColor = new Color(85f/255f, 98f/255f, 112f/255f);
                bullet1Color = new Color(199f/255f, 244f/255f, 100f/255f);
                bullet2Color = new Color(255f / 255f, 107f / 255f, 107f / 255f);
                background1Color = new Color(78f / 255f, 205f / 255f, 196f / 255f); 
                background2Color = playerColor;
                break;
            }
            case 2:
                {
                    playerColor = new Color(66f / 255f, 66f / 255f, 66f / 255f);
                    bullet1Color = new Color(255f / 255f, 153f / 255f, 0f / 255f); 
                    bullet2Color = new Color(50f / 255f, 153f / 255f, 187f / 255f);
                    background1Color = new Color(188f / 255f, 188f / 255f, 188f / 255f);
                    background2Color = playerColor;
                    break;
                }
            case 3:
                {
                    playerColor = new Color(46f / 255f, 38f / 255f, 51f / 255f);
                    bullet1Color = new Color(239f / 255f, 255f / 255f, 205f / 255f);
                    bullet2Color = new Color(153f / 255f, 23f / 255f, 60f / 255f);
                    background1Color = new Color(85f / 255f, 23f / 255f, 60f / 255f);
                    background2Color = playerColor;
                    break;
                }
            case 4:
                {
                    playerColor = new Color(28f / 255f, 20f / 255f, 13f / 255f);
                    bullet1Color = new Color(255f / 255f, 255f / 255f, 255f / 255f);
                    bullet2Color = new Color(203f / 255f, 232f / 255f, 107f / 255f);
                    background1Color = new Color(242f / 255f, 233f / 255f, 225f / 255f);
                    background2Color = playerColor;
                    break;
                }
	    }
	}

    public void ChangeAllObjectsToMatchPallete(int i)  {
        InitPalleteNum(i);
        StartCoroutine(Manager.colorManager.GetComponent<ColorPalette>().changeBackground1());
        StartCoroutine(Manager.colorManager.GetComponent<ColorPalette>().changeBackground2());
        StartCoroutine(Manager.colorManager.GetComponent<ColorPalette>().changeCamera());
    }

    public IEnumerator changeCamera() {
        while (Camera.main.backgroundColor != background1Color)
        {
            yield return new WaitForEndOfFrame();
            float t = Time.deltaTime * 10;
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, background1Color, t);
        }
    }

    public IEnumerator changeBackground2()
    {
        
        foreach (var pl in GameObject.FindGameObjectsWithTag("Background2"))
        {
            SpriteRenderer spriteRenderer = pl.GetComponent<SpriteRenderer>();
            while (spriteRenderer.color != background2Color)
            {
                yield return new WaitForEndOfFrame();
                float t = Time.deltaTime *  10;
                spriteRenderer.color = Color.Lerp(spriteRenderer.color, background2Color, t);
            }
        }
    }

    public IEnumerator changeBackground1()
    {
        foreach (var pl in GameObject.FindGameObjectsWithTag("Background1"))
        {
            SpriteRenderer spriteRenderer = pl.GetComponent<SpriteRenderer>();
            while (spriteRenderer.color != background1Color)
            {
                yield return new WaitForEndOfFrame();
                float t = Time.deltaTime * 10;
                spriteRenderer.color = Color.Lerp(spriteRenderer.color, background1Color, t);
            }
        }
    }
    
}
