using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorPillar : MonoBehaviour {

    // Current State of the Floor
    private FloorState _floorState;

    public FloorState floorState
    {
        get { return _floorState;  }
        set
        {
            if (value == FloorState.RISING && _floorState == FloorState.DOWN)
            {
                // pass
            }
            else if (value == FloorState.FALLING && _floorState == FloorState.UP)
            {
                // pass
            } else
            {
                Debug.Log("WARNING: Illegal Parameter / State for FloorPillar.floorState.set()" + value + ", " + _floorState);
            }

            _floorState = value;
        }
    }

    // How much the floorPillar have to rise / fall to change state
    private const float RISING_THRESHOLD = -2.5f;
    private const float FALLING_THRESHOLD = -7.6f;

    // Moving Speed of FloorPillar on Transition 
    private const float MOVING_SPEED = 10.0f;
    
    // Direction of "UP" 
    private Vector3 UP_DIRECTION = Vector3.up;

    private Vector3 ORTHO_COMP;

    // Get the current floor height from object's transformation
    public float GetFloorHeight()
    {
        return Vector3.Dot(transform.position, UP_DIRECTION);
    }

    // Increases floor height (transform). If height reaches a threshold, then change floor state.
    private void IncreaseHeightUntil()
    {
        transform.position += UP_DIRECTION * MOVING_SPEED * Time.deltaTime;
        if (GetFloorHeight() >= RISING_THRESHOLD)
        {
            transform.position = RISING_THRESHOLD * UP_DIRECTION + ORTHO_COMP;
            _floorState = FloorState.UP;
        }
    }

    // Decreases floor height (transform). If height reaches a threshold, then change floor state.
    private void DecreaseHeightUntil()
    {
        transform.position -= UP_DIRECTION * MOVING_SPEED * Time.deltaTime;
        if (GetFloorHeight() <= FALLING_THRESHOLD)
        {
            transform.position = FALLING_THRESHOLD * UP_DIRECTION + ORTHO_COMP;
            _floorState = FloorState.DOWN;
        }
    }

    // Changes this FloorPillar from UP to FALLING and DOWN to RISING
    public void Toggle()
    {
        if (_floorState == FloorState.UP)
        {
            _floorState = FloorState.FALLING;
        } else if (_floorState == FloorState.DOWN)
        {
            _floorState = FloorState.RISING;
        } else
        {
            Debug.LogError("Cannot call FloorPillar.Toggle() in current state");
        }
    }

    // Use this for initialization
    void Start () {
        ORTHO_COMP = Vector3.ProjectOnPlane(transform.position, UP_DIRECTION);
        _floorState = FloorState.UP;
        transform.position = UP_DIRECTION * RISING_THRESHOLD + ORTHO_COMP;
    }
	
	// Update is called once per frame
	void Update () {
		switch(floorState)
        {
            case FloorState.UP:
                break;
            case FloorState.FALLING:
                DecreaseHeightUntil();
                break;
            case FloorState.DOWN:
                break;
            case FloorState.RISING:
                IncreaseHeightUntil();
                break;
        }
	}
}
