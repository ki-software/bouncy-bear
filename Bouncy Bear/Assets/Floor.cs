using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {

    public FloorPillar[] floorPillars;
    // private int floorPillarCount = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public FloorPillar GetRandomSpace()
    {
        List<FloorPillar> f = new List<FloorPillar>();
        foreach (FloorPillar fp in floorPillars)
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
        List<FloorState> retval = new List<FloorState>(new FloorState[floorPillars.Length]);
        List<int> unselected = new List<int>();
        List<int> selected = new List<int>();

        // Populate unselected list
        for (int i = 0; i < floorPillars.Length; i++)
        {
            unselected.Add(i);
        }

        int selectionCount = 3;

        if (selectionCount >= floorPillars.Length)
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
        if (next.Count != floorPillars.Length)
        {
            Debug.LogError("Next Array Size != Current Array Size");
        }

        for (int i = 0; i < next.Count; i++)
        {
            if (next[i] == FloorState.DOWN && (floorPillars[i].floorState == FloorState.UP || floorPillars[i].floorState == FloorState.RISING))
            {
                floorPillars[i].floorState = FloorState.FALLING;
            }
            else if (next[i] == FloorState.UP && (floorPillars[i].floorState == FloorState.DOWN || floorPillars[i].floorState == FloorState.FALLING))
            {
                floorPillars[i].floorState = FloorState.RISING;
            }
        }
    }
}
