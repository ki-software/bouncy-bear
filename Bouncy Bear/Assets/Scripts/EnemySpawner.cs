using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    public GameObject[] enemies;

	// Use this for initialization
	void Start () {
		if (enemies.Length == 0)
        {
            Debug.LogError("Enemies array cannot be empty!");
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	}

    public GameObject GetRandomEnemyType()
    {
        int idx = Random.Range(0, enemies.Length - 1);
        return enemies[idx];
    }

    public GameObject SpawnEnemy(Object originalEnemy, Vector3 location)
    {
        GameObject retval = Instantiate(originalEnemy, location, Quaternion.identity) as GameObject;
        Debug.Log("Spawned");
        return retval;
    }
}
