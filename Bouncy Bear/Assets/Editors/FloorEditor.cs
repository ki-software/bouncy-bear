using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Floor))]
public class FloorEditor : Editor {


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        Floor floor = target as Floor;

        // 
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Add Pillar"))
        {
            floor.AddFloorPillar();
        }
        if (GUILayout.Button("Remove Pillar"))
        {
            floor.RemoveFloorPillar();
        }
        if (GUILayout.Button("Debug Pillar"))
        {
            floor.DebugFloorPillar();
        }

        EditorGUILayout.EndHorizontal();

    }

}
