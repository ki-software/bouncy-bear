using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public static GameController GC;

	private GameObject playerObject;
	private Player player;

	// Use this for initialization
	void Start () {
		GC = this;

		playerObject = GameObject.FindGameObjectWithTag ("Player");
		player = playerObject.GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayerHitWall() {
		player.BounceUpAway ();
	}
}
