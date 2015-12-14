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
                playerColor = new Color(84f/255f, 36f/255f, 55f/255f);
                bullet1Color = new Color(217f/255f, 91f/255f, 67f/255f);
                bullet2Color = new Color(83f/255f, 119f/255f, 122f/255f);
                background1Color = new Color(192f/255f, 41f/255f, 66f/255f);
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
	    }
	}

    IEnumerator ChangeAllObjectsToMatchPallete()  {
        foreach (var pl in GameObject.FindGameObjectsWithTag("PlayerColor")) {
            
        }
        yield return new WaitForEndOfFrame();
    }
    
}
