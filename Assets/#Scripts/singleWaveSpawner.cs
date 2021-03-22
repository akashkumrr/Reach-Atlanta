using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class singleWaveSpawner : MonoBehaviour
{

    public Transform playerTargetTransform;
    public bool allWavesSpawned=false;
    public bool isSpawning=false;
    public float mainTimer = 0;
    public float[] startTimes;
    public int index = 0;
    public GameObject enemyObject;
    public float spawnRate;
    public float totalSpawningTime;
    public float countingSpawningTime;
    public float timer = 0;
    public Vector3 perpendicularDirection;

    public levelManager theLevelMan;
    // Start is called before the first frame update
    void Start()
    {
        theLevelMan = Object.FindObjectOfType<levelManager>();
        if (startTimes[startTimes.Length - 1] + totalSpawningTime >= theLevelMan.lastSpawnTime)
        {
            theLevelMan.lastSpawnTime = startTimes[startTimes.Length - 1] + totalSpawningTime;
        }

    }

    // Update is called once per frame
    void Update()
    {
        mainTimer += Time.deltaTime;

       

        if(index < startTimes.Length && mainTimer > startTimes[index] )
        {
            isSpawning = true;
            index++;
        }


        if (isSpawning == false)
        {
            timer = 0;
            countingSpawningTime = 0;
        }
        else if (isSpawning == true && countingSpawningTime <= totalSpawningTime)
        {
            timer += Time.deltaTime;
            countingSpawningTime += Time.deltaTime;
            if (timer  >=1/spawnRate)
            {
                perpendicularDirection = Random.Range(-10, 10) * (Quaternion.Euler(0, 90, 0) *(transform.position - playerTargetTransform.position).normalized);
                GameObject g = Instantiate(enemyObject, this.gameObject.transform.position + perpendicularDirection , Quaternion.identity);
                theLevelMan.liveZombieCount += 1;
                g.name = enemyObject.name + this.gameObject.name;
                g.transform.position = transform.position + perpendicularDirection;   //  Instantiate just ignores whatever position I enter and only instantiates at the prefabs location
                // not writing above line causes . causes what??? causes for some weird reason to spawn PREFAB on the some position and ignore the spawning position.


                g.SetActive(true);

                timer =0;
            }
        }
        else
        {
            isSpawning = false; // Stop spawning if totalSpawningTime is reached
            timer = 0;
            countingSpawningTime = 0;
        }

        Debug.DrawRay(transform.position, perpendicularDirection, Color.red);

    }
}
