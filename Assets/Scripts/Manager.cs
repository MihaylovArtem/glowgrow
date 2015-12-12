using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour
{
    private float score;
    private float time;
    private int stage;
    private ColorPalette.Colors stagePalette;
	// Use this for initialization
	void Start ()
	{
	    score = 0;
	    time = 0;
	    stage = 1;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}
}
