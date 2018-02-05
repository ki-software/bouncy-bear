using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float acceleration = 1;
	public float gravity = 10;
	public float verticalVelocity = 1;
	public float horizontalDrag = 0.01f;

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
		}
		rb.AddForce (-Mathf.Sign(rb.velocity.x) * horizontalDrag, 0, 0);
	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Floor")) {
			rb.velocity = new Vector3 (rb.velocity.x, verticalVelocity, 0);
		}
	}
}
