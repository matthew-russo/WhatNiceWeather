using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RandomizationKit;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject slowZombie;
    public GameObject fastZombie;

    public float spawnTimer = 3f;

    public List<GameObject> spawnPositions;

    private void Start()
    {
        foreach (Transform child in transform)
        {
            spawnPositions.Add(child.gameObject);
        }
    }
	
	void Update () {
	    if (spawnTimer > 0)
	    {
	        spawnTimer -= Time.deltaTime;
	    }
	    else
	    {
	        SpawnZombie(Random.Range(1, 3));
	    }
	}

    void SpawnZombie(int zombieType)
    {
        RandomFuncs.FYShuffle(spawnPositions);
        Transform spawnPos = spawnPositions[0].transform;
        if (zombieType == 1)
        {
            Instantiate(slowZombie, spawnPos.position, spawnPos.rotation, spawnPos);
        }
        else
        {
            Instantiate(fastZombie, spawnPos.position, spawnPos.rotation, spawnPos);
        }
        spawnTimer = -Mathf.Log((GameManager.Instance.killCount * 3) + 1) + 6;
    }
}
