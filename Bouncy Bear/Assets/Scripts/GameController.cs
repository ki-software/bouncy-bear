using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController GC;

	private GameObject playerObject;
	private Player player;
	private GameObject floorObject;
	private Floor floor;

	private uint score;
	private uint combo;

	// Use this for initialization
	void Start () {
		GC = this;

		InitObject ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void PlayerHitFloor() {
		ChangeFloor ();
		player.BounceFloor ();
	}

	public void PlayerHitWall() {
		player.BounceWall ();
	}

	public void PlayerHitCoin() {
	
	}

	public void PlayerDie() {
		
	}


	void InitObject() {
		playerObject = GameObject.FindGameObjectWithTag ("Player");
		player = playerObject.GetComponent<Player> ();

		floorObject = GameObject.FindGameObjectWithTag ("Floor");
		floor = floorObject.GetComponent<Floor> ();
	}

	void ChangeFloor() {
		List<FloorState> floorState = floor.GenerateRandomFloor (6);
		floor.ChangeFloor (floorState);
	}
}
