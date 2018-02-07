using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour {

    private Countdown countdown;
    private EnemyState _state;
    public EnemyState state
    {
        get { return _state;  }
    }

    private float MOVING_SPEED = 10.0f;
    private float RISING_THRESHOLD = 4.0f;
    private float FALLING_THRESHOLD = -2.0f;

    private Vector3 ORTHO_COMP;

    // Use this for initialization
    void Start () {
        _state = EnemyState.INITIAL;
        // Initialize Countdown Object
        countdown = transform.gameObject.GetComponentInChildren<Countdown>();
        if (countdown == null || countdown.gameObject.transform.parent.gameObject != gameObject)
        {
            Debug.LogError("Enemy does not contain corresponding Countdown Object");
        }
        ORTHO_COMP = Vector3.ProjectOnPlane(transform.position, Vector3.up);
        Debug.Log("Enemy ORTHO_COMP = " + ORTHO_COMP);
    }
	
	// Update is called once per frame
	void Update () {
		switch(_state)
        {
            case EnemyState.INITIAL:
                _state = EnemyState.ENTERING;
                break;
            case EnemyState.ENTERING:
                IncreaseHeightUntil();
                break;
            case EnemyState.PREPARING:
                break;
            case EnemyState.ACTING:
                // temporary hack
                StartAction();
                break;
            case EnemyState.EXITING:
                DecreaseHeightUntil();
                break;
            case EnemyState.DESTROY:
                Destroy(gameObject);
                break;
        }
	}

    private float GetFloorHeight()
    {
        return transform.position.y;
    }

    private void IncreaseHeightUntil()
    {
        transform.position += Vector3.up * MOVING_SPEED * Time.deltaTime;
        if (GetFloorHeight() >= RISING_THRESHOLD)
        {
            transform.position = RISING_THRESHOLD * Vector3.up + ORTHO_COMP;
            _state = EnemyState.PREPARING;
            NotifyEnterFinished();
        }
    }

    private void DecreaseHeightUntil()
    {
        transform.position -= Vector3.up * MOVING_SPEED * Time.deltaTime;
        if (GetFloorHeight() <= FALLING_THRESHOLD)
        {
            transform.position = FALLING_THRESHOLD * Vector3.up + ORTHO_COMP;
            _state = EnemyState.DESTROY;
        }
    }

    protected void NotifyEnterFinished()
    {
        countdown.StartCountdown();
    }

    public void OnNotifyPrepareFinished()
    {
        if (_state != EnemyState.PREPARING)
        {
            Debug.LogError("Invalid State Transition");
        }
        _state = EnemyState.ACTING;
    }

    public void StartAction()
    {
        if (_state != EnemyState.ACTING)
        {
            Debug.LogError("Invalid State Transition");
        }
        StartCoroutine(Act());
        
    }
    
    private IEnumerator Act()
    {
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.red;
        yield return new WaitForSeconds(5);
        gameObject.GetComponentInChildren<Renderer>().material.color = Color.grey;
        _state = EnemyState.EXITING;
    }
}
