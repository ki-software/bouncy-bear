using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float acceleration = 1;
	public float gravity = 10;
	public float verticalVelocity = 1;
	public float horizontalDrag = 0.01f;
	public float bounceVelocity = 1;

	private PlayerMovementState _moveState = PlayerMovementState.FALLING;

	public PlayerMovementState moveState
	{
		get { return _moveState;  }
		set
		{
			if (value == PlayerMovementState.FALLING &&
				(_moveState == PlayerMovementState.RISING)) {
				// pass
			}
			else if (value == PlayerMovementState.RISING &&
					(_moveState == PlayerMovementState.POST_BOUNCEDOWN &&
					 _moveState == PlayerMovementState.POST_BOUNCELEFT &&
					 _moveState == PlayerMovementState.POST_BOUNCERIGHT)) {
				// pass
			} 
			else if (value == PlayerMovementState.DYING &&
					(_moveState == PlayerMovementState.FALLING &&
					 _moveState == PlayerMovementState.POST_BOUNCELEFT &&
					 _moveState == PlayerMovementState.POST_BOUNCERIGHT)) {
				// pass
			} 
			else if (value == PlayerMovementState.PRE_BOUNCEDOWN &&
					(_moveState == PlayerMovementState.FALLING)) {
				// pass
			}
			else if (value == PlayerMovementState.POST_BOUNCEDOWN &&
					(_moveState == PlayerMovementState.PRE_BOUNCEDOWN)) {
				// pass
			}
			else if (value == PlayerMovementState.PRE_BOUNCELEFT &&
					(_moveState == PlayerMovementState.FALLING &&
					 _moveState == PlayerMovementState.RISING &&
					 _moveState == PlayerMovementState.DYING)) {
				// pass
			}
			else if (value == PlayerMovementState.POST_BOUNCELEFT &&
					(_moveState == PlayerMovementState.PRE_BOUNCELEFT)) {
				// pass
			}
			else if (value == PlayerMovementState.PRE_BOUNCERIGHT &&
					(_moveState == PlayerMovementState.FALLING &&
					 _moveState == PlayerMovementState.RISING &&
					 _moveState == PlayerMovementState.DYING)) {
				// pass
			}
			else if (value == PlayerMovementState.POST_BOUNCERIGHT &&
					(_moveState == PlayerMovementState.PRE_BOUNCERIGHT)) {
				// pass
			}
			else {
				Debug.Log("WARNING: Illegal Parameter / State for Player.moveState.set()" + value + ", " + _moveState);
			}

			_moveState = value;
		}
	}

	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CheckInput ();
		Fall ();
		Drag ();
	}

	void CheckInput() {
		rb.AddForce (Input.GetAxisRaw("Horizontal") * acceleration, 0, 0);
	}

	void Fall() {
		rb.AddForce (0, -gravity, 0);
	}

	void Drag() {
		if (Mathf.Abs(rb.velocity.x) < 0.0001) {
			rb.velocity = new Vector3 (0, rb.velocity.y, 0);
			return;
		}
		rb.AddForce (-Mathf.Sign(rb.velocity.x) * horizontalDrag, 0, 0);
	}

	void OnTriggerEnter(Collider other) {
		if (other.transform.root.CompareTag("Floor")) {
			GameController.GC.PlayerHitFloor ();
		}
		else if (other.transform.root.CompareTag("Coin")) {
			GameController.GC.PlayerHitCoin ();
			other.transform.root.gameObject.GetComponent<Coin>().Collected ();
		}
	}

	public void BounceFloor() {
		rb.velocity = new Vector3 (rb.velocity.x, verticalVelocity, 0);
	}

	public void BounceWall() {
		rb.velocity = new Vector3 (-Mathf.Sign(rb.velocity.x), 0.5f, 0) * bounceVelocity;
	}
}
