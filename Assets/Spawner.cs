using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public GameObject[] spawners;

    public GameObject[] zombies;

    public GameObject nearestSpawn;

    public GameObject player;

    public GameObject clone;

    public GameObject Zombie;

    public float distance=0;

    public float spawnTime = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawners = GameObject.FindGameObjectsWithTag("spawnpoint");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        spawnTime -= Time.deltaTime;
 
        if (spawnTime <= 0.0f)
        {          
            zombies = GameObject.FindGameObjectsWithTag("Regular_Zombie");

            nearestSpawn = GetClosestSpawn(spawners);
            distance = Vector3.Distance(player.transform.position, nearestSpawn.transform.position);
            spawnTime = 10f;
            if(distance < 100 && distance > 20 && zombies.Length < 3){
                clone = Instantiate(Zombie, nearestSpawn.transform.position, nearestSpawn.transform.rotation);               
            }       
        }
    }

    GameObject GetClosestSpawn(GameObject[] spawners)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = player.transform.position;
        foreach (GameObject go in spawners)
        {
            float dist = Vector3.Distance(go.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = go;
                minDist = dist;
            }
        }
        Debug.Log("tMin : " + tMin);
        return tMin;
    }
}
