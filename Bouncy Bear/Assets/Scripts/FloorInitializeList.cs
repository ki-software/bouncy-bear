using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class FloorInitializeList : MonoBehaviour {

    // Use this for initialization
    void Start()
    {
        List<FloorPillar> _floorPillars = gameObject.GetComponent<Floor>().floorPillars;
        foreach (Transform tr in transform)
        {
            FloorPillar fp = tr.gameObject.GetComponent<FloorPillar>();
            if (fp != null)
            {
                _floorPillars.Add(fp);
            }
        }
        Debug.Log("FloorPillars.Count = " + _floorPillars.Count);
    }
    
}
