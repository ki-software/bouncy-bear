using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SerializeField]
public class FloorTestDriver : MonoBehaviour {

    public Floor floor;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            List<FloorState> ls = floor.GenerateRandomFloor(3);
            floor.ChangeFloor(ls);
        }
    }
}
