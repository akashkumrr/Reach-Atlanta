using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelManager : MonoBehaviour
{

    public bool allZombiesSpawned = false;

    public int totalZombiesInLevel;
    public int liveZombieCount;
    public float lastSpawnTime = -1f;
    public float timer = 0;

    public delegate void missionComplete();
    public static event missionComplete onMissionComplete;
    // Start is called before the first frame update
    void Start()
    {
        zombieHealth.onZombieDeath += decreaseLiveZombieCountByOne;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        
    }

    public void decreaseLiveZombieCountByOne()
    {
        Debug.Log("Zombie count reducde by 1");
        liveZombieCount -= 1;

        if (liveZombieCount <= 0 && timer >= lastSpawnTime)
        {
            Debug.Log("Mission Completed");
            if(onMissionComplete!=null)
            {
                onMissionComplete();
            }
            // a button should appear that says continue and it would move on the next level
        }
    }
}
