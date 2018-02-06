using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    public GameObject floorPillar;

    private List<FloorPillar> _floorPillars = new List<FloorPillar>();
    public List<FloorPillar> floorPillars
    {
        get { return _floorPillars; }
    }

    public void AddFloorPillar()
    {
        int n = _floorPillars.Count;
        GameObject newObj = Instantiate<GameObject>(floorPillar, Vector3.zero, Quaternion.identity, gameObject.transform);
        newObj.transform.localPosition = new Vector3(n, 0, 0);
        newObj.name = "FloorPillar (" + n + ")";
        _floorPillars.Add(newObj.GetComponent<FloorPillar>());
    }
    public void RemoveFloorPillar()
    {
        GameObject rmObj = _floorPillars[_floorPillars.Count - 1].gameObject;
        _floorPillars.RemoveAt(_floorPillars.Count - 1);
        DestroyImmediate(rmObj);
    }
    public void DebugFloorPillar()
    {
        Debug.Log("floorPillars.Count = " + floorPillars.Count);
    }
    // private int floorPillarCount = 10;
	
	// Update is called once per frame
	void Update () {

	}

    public FloorPillar GetRandomSpace()
    {
        List<FloorPillar> f = new List<FloorPillar>();
        foreach (FloorPillar fp in _floorPillars)
        {
            if (fp.floorState == FloorState.DOWN)
            {
                f.Add(fp);
            } 
        }

        int rand = Random.Range(0, f.Count - 1);
        return f[rand];
    }

    public List<FloorState> GenerateRandomFloor(int voids)
    {
        List<FloorState> retval = new List<FloorState>(new FloorState[_floorPillars.Count]);
        List<int> unselected = new List<int>();
        List<int> selected = new List<int>();

        // Populate unselected list
        for (int i = 0; i < _floorPillars.Count; i++)
        {
            unselected.Add(i);
        }

        int selectionCount = 3;

        if (selectionCount >= _floorPillars.Count)
        {
            Debug.LogError("Invalid selection count!");
        }

        while(selected.Count < selectionCount)
        {
            int rand = Random.Range(0, unselected.Count);
            selected.Add(unselected[rand]);
            unselected.RemoveAt(rand);
        }

        foreach(int idx in unselected)
        {
            retval[idx] = FloorState.UP;
        }

        foreach (int idx in selected)
        {
            retval[idx] = FloorState.DOWN;
        }

        return retval;
    }

    public void ChangeFloor(List<FloorState> next)
    {
        if (next.Count != _floorPillars.Count)
        {
            Debug.LogError("Next Array Size != Current Array Size");
        }

        for (int i = 0; i < next.Count; i++)
        {
            if (next[i] == FloorState.DOWN && (_floorPillars[i].floorState == FloorState.UP || _floorPillars[i].floorState == FloorState.RISING))
            {
                _floorPillars[i].floorState = FloorState.FALLING;
            }
            else if (next[i] == FloorState.UP && (_floorPillars[i].floorState == FloorState.DOWN || _floorPillars[i].floorState == FloorState.FALLING))
            {
                _floorPillars[i].floorState = FloorState.RISING;
            }
        }
    }
}
