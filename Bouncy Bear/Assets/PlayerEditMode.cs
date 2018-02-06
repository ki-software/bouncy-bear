using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class PlayerEditMode : MonoBehaviour {

	// Make Player Active on load
	void Awake () {
        gameObject.SetActive(true);
	}
	
}
