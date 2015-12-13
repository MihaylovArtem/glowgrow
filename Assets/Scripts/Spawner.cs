using System.Security.Policy;
using Microsoft.Win32;
using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour
{
   
    public GameObject patternGameObject;

    public static float curTime;
    public static int objectWithPatternCreated;

    private bool isGuidePatternShown;

    private void GuidePattern()
    {

        
    }

    void Pattern1()
    {
        
    }
	// Use this for initialization
	void Start ()
	{
	    //InvokeRepeating("SpawnSingleBullet", 2f, 1f);
        //Invoke("GuidePattern", 2f);
	    curTime = 0f;
	    objectWithPatternCreated = 0;
	    isGuidePatternShown = false;
	    //var guidePattern = (GameObject)Instantiate(patternGameObject, Vector3.zero, Quaternion.identity);
	    //guidePattern.GetComponent<Pattern>().patternNumber = 1;
	    //bulletColor = <берем из палитры >;
	    //int k = 0;
	}
	
	// Update is called once per frame

   
	void Update ()
	{
	    curTime += Time.deltaTime;
	    if (curTime >= 2f && !isGuidePatternShown)
	    {
	        curTime = 0;
	        isGuidePatternShown = true;
            var guidePattern = (GameObject)Instantiate(patternGameObject, Vector3.zero, Quaternion.identity);
            guidePattern.GetComponent<Pattern>().patternNumber = 1;
	    }
	}
}
