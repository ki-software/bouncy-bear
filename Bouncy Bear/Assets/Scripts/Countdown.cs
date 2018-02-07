using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Countdown : MonoBehaviour {

    private EnemyBase parent;
    private float baseSize;
    public float countdownTime;
    private float startingTime;

    private Vector3 originalScale;
    private Vector3 finalScale;

    private CountdownState _state;
    public CountdownState state
    {
        get { return _state; }
    }

	// Use this for initialization
	void Start () {
        _state = CountdownState.INIT;
        parent = transform.parent.GetComponentInChildren<EnemyBase>();
        originalScale = transform.localScale;
        finalScale = new Vector3(0.9f, 0.9f, 0.9f);
        if (countdownTime <= 0.0f)
        {
            Debug.LogError("Invalid Countdown Time value (countdownTime = " + countdownTime + ")");
        }
    }
	
	// Update is called once per frame
	void Update () {
		switch(_state) {
            case CountdownState.INIT:
                break;
            case CountdownState.ACTIVE:
                float timePassed = Time.time - startingTime;
                float scalingFactor = timePassed / countdownTime;

                if (scalingFactor >= 1.0f)
                {
                    _state = CountdownState.DESTROY;
                    NotifyPrepareFinish();
                }

                transform.localScale = Vector3.Lerp(originalScale, finalScale, scalingFactor);

                break;
            case CountdownState.DESTROY:
                break;
        }
	}

    public void StartCountdown()
    {
        startingTime = Time.time;
        _state = CountdownState.ACTIVE;
    }

    public void NotifyPrepareFinish()  
    {
        Debug.Log("Notified Finish");
        parent.OnNotifyPrepareFinished();
    }
}
