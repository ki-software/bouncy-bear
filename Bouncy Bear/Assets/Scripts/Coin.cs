using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

	public float lifeTime = 1;
	public float rotationSpeed = 90;
	public float height = 1;
	public float verticalVelocity = 1;

	private float deathTime;
	private CoinState _state = CoinState.INIT;

	public CoinState state {
		get { return _state; }
		set {
			if (value == CoinState.RISING &&
				(_state == CoinState.INIT)) {
				
			}
			else if (value == CoinState.STAY &&
					 (_state == CoinState.RISING)) {
				
			}
			else if (value == CoinState.FALLING &&
					 (_state == CoinState.STAY)) {
				
			}
			else {
				Debug.Log("WARNING: Illegal Parameter / State for Coin.state.set()" + value + ", " + _state);
			}
			_state = value;
		}
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Rotate ();
		CheckState ();
	}

	void Rotate() {
		transform.Rotate (0, rotationSpeed * Time.deltaTime, 0);
	}

	void CheckState() {
		if (state == CoinState.INIT) {
			
			state = CoinState.RISING;
		}
		else if (state == CoinState.RISING) {
			transform.position = new Vector3 (transform.position.x, transform.position.y + verticalVelocity * Time.deltaTime,0);

			if (transform.position.y >= height) {
				deathTime = Time.time + lifeTime;
				transform.position = new Vector3 (transform.position.x, height,0);

				state = CoinState.STAY;
			}
		}
		else if (state == CoinState.STAY) {
			if (Time.time >= deathTime) {
				state = CoinState.FALLING;
			}
		}
		else if (state == CoinState.FALLING) {
			transform.position = new Vector3 (transform.position.x, transform.position.y - verticalVelocity * Time.deltaTime,0);

			if (transform.position.y <= -4) {
				Destroy (this.gameObject);
			}
		}
	}

	public void Collected() {
		Debug.Log ("Coin: Collected!");
		Destroy (this.gameObject);
	}
}
