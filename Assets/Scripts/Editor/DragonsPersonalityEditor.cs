using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DragonsPersonality))]
public class DragonsPersonalityEditor : Editor
{
    private static int _amount = 200;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DragonsPersonality myScript = (DragonsPersonality) target;
        EditorGUILayout.LabelField("Mood", myScript.Mood.ToString());
    }
}