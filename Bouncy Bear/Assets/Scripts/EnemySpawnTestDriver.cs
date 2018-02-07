using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTestDriver : MonoBehaviour {

    public EnemySpawner es;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.K))
        {
            es.SpawnEnemy(es.GetRandomEnemyType(), new Vector3(3.5f, -2.0f, 0.1f));
        }
	}
}
