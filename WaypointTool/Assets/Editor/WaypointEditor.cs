using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

[CanEditMultipleObjects]
[CustomEditor(typeof(Character))]
public class WaypointEditor : Editor
{
    private SerializedObject so;
    private SerializedProperty propWayPoints;
    private SerializedProperty propRepeatPath;
    private SerializedProperty propWaypointObject;

    private Character character;

    private bool addingWaypoint;

    private ReorderableList wayPointList;

    private void OnEnable()
    {
        so = serializedObject;

        character = (Character) target;

        propWayPoints = so.FindProperty("wayPoints");
        propRepeatPath = so.FindProperty("repeatPath");

        wayPointList = new ReorderableList(so, propWayPoints, true, true, true, true);

        wayPointList.drawElementCallback = DrawListItems;
        wayPointList.drawHeaderCallback = DrawHeader;

        SceneView.duringSceneGui += OnSceneGUI;

    }

    void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty
            Element = wayPointList.serializedProperty.GetArrayElementAtIndex(index); // The element in the list

        EditorGUI.LabelField(new Rect(rect.x, rect.y, 100, EditorGUIUtility.singleLineHeight), "Waypoint " + index);

        EditorGUILayout.Space();

        EditorGUI.PropertyField(new Rect(new Rect(rect.x + 100, rect.y, 300, EditorGUIUtility.singleLineHeight)),
            propWayPoints.GetArrayElementAtIndex(index), GUIContent.none);
    }

    void DrawHeader(Rect rect)
    {
        string name = "Waypoints";
        EditorGUI.LabelField(rect, name);
    }

    //Draw in inspector
    public override void OnInspectorGUI()
    {
        so.Update();

        wayPointList.DoLayoutList();

        propRepeatPath.boolValue = EditorGUILayout.Toggle("Repeat path", propRepeatPath.boolValue);

        if (GUILayout.Button("Add waypoint"))
        {
            addingWaypoint = true;
            Debug.Log(addingWaypoint);
        }

        if (GUILayout.Button("Stop editing waypoints"))
        {
            addingWaypoint = false;
            Debug.Log(addingWaypoint);
        }

        if (so.ApplyModifiedProperties())
        {
            SceneView.RepaintAll();
        }
    }

    //Draw in the scene
    private void OnSceneGUI(SceneView sceneView)
    {
        Handles.color = Color.green;
        so.Update();

        for (int i = 0; i < propWayPoints.arraySize; i++)
        {
            SerializedProperty prop = propWayPoints.GetArrayElementAtIndex(i);
            prop.vector3Value = Handles.PositionHandle(prop.vector3Value, Quaternion.identity); // get vector3 point
            
            Handles.Label(prop.vector3Value, "Waypoint: " + i);
        }

        for (int i = 0; i < propWayPoints.arraySize; i++) //Draw dotted line between waypoints
        {
            if (propWayPoints.GetArrayElementAtIndex(i) != null)
            {
                Handles.DrawDottedLine(propWayPoints.GetArrayElementAtIndex(i).vector3Value,
                    propWayPoints.GetArrayElementAtIndex((int) Mathf.Repeat(i + 1, propWayPoints.arraySize))
                        .vector3Value, 15f);
            }
        }

        Event e = Event.current;
        Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
        
        if (addingWaypoint)
        {
            if (e.type == EventType.MouseUp && e.button == 1) //If add waypoint button has been clicked and a point in the world has been clicked
            {
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.point);
                    character.CreateWaypoint(hit.point);
                    addingWaypoint = false;
                }
            }
        }

        so.ApplyModifiedProperties();
    }

    private void OnDisable()
    {
        SceneView.duringSceneGui -= OnSceneGUI;
    }
}