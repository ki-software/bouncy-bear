using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinGenerator : MonoBehaviour {

	public static CoinGenerator CG;
	public GameObject coin;

	private bool generatable = true;
	private GameObject currentCoin;

	// Use this for initialization
	void Start () {
		CG = this;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void GenerateAt(float posX) {
		currentCoin = Instantiate (coin);
		currentCoin.transform.position = new Vector3 (posX, coin.transform.position.y, 0);

		generatable = false;
	}

	public bool CheckGeneratable() {
		if (generatable == false && currentCoin == null) {
			generatable = true;
			return false;
		}
		return generatable;
	}
}
