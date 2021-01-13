using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDistortion : MonoBehaviour
{
    public Kino.AnalogGlitch analogGlitch;
    public LightingManager lightingManager;
    public GameObject player;
    public GameObject[] lightSources;

    public GameObject nearestLight;
    public float distance=0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        lightSources = GameObject.FindGameObjectsWithTag("LightSource");
    }

    // Update is called once per frame
    void Update()
    {
        //lightSources = GameObject.FindGameObjectsWithTag("LightSource");

        nearestLight = GetClosestLightSource(lightSources);
        distance = Vector3.Distance(player.transform.position, nearestLight.transform.position);

        //Debug.Log("Distance = " + distance);

        if(lightingManager.TimeOfDay < 175 || lightingManager.TimeOfDay > 775){

            if(distance > 20f){
                //Debug.Log("DISTORT");
                analogGlitch.scanLineJitter = 0.4f;  
                analogGlitch.colorDrift = 0.1f; 
            }
            else{
                //Debug.Log("STOP");
                analogGlitch.scanLineJitter = 0;
                analogGlitch.colorDrift = 0; 
            }

        }
        else{
            //Debug.Log("STOP");
            analogGlitch.scanLineJitter = 0;
            analogGlitch.colorDrift = 0; 
        }
        
    }

    GameObject GetClosestLightSource(GameObject[] lightSources)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject go in lightSources)
        {
            float dist = Vector3.Distance(go.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = go;
                minDist = dist;
            }
        }
        //Debug.Log("tMin : " + tMin);
        return tMin;
    }
}
