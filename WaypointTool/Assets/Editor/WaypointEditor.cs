using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(Character))]
public class WaypointEditor : Editor
{
    private Character character;

    private SerializedObject so;
    private SerializedProperty wayPoints;
    

    private void OnEnable()
    {
        so = serializedObject;

        character = (Character) target;
        
        wayPoints = so.FindProperty("wayPoints");

        Selection.selectionChanged += Repaint;
    }

    //Draw in inspector
    public override void OnInspectorGUI()
    {
        so.Update();
        
        EditorGUILayout.PropertyField(wayPoints);
        
        if (so.ApplyModifiedProperties())
        {
            Repaint();
        }
    }

    //Draw in the scene
    private void OnSceneGUI()
    {
        Handles.color = Color.green;

        for (int i = 0; i < character.wayPoints.Length; i++)
        {
           character.wayPoints[i] = Handles.PositionHandle(character.wayPoints[i], Quaternion.identity);
        }

        for (int i = 0; i < character.wayPoints.Length; i++)
        {
            if (character.wayPoints[i] != null)
            {
                Handles.DrawDottedLine(character.wayPoints[i], character.wayPoints[(int)Mathf.Repeat(i + 1, character.wayPoints.Length)], 15f);
            }
        }
    }

    private void GetWaypoints()
    {
    }
}
