using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(LootDropper))]
public class LootDropperEditor : Editor
{
    private static int _amount = 200;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!EditorApplication.isPlaying)
            return;

        LootDropper myScript = (LootDropper) target;
        _amount = EditorGUILayout.IntField("Amount", _amount);

        if (GUILayout.Button("Drop"))
        {
            myScript.Drop(_amount);
            Debug.Log("Dropped " + _amount);
        }
    }
}