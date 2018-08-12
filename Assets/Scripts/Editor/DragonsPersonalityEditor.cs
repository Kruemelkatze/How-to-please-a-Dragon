using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DragonsPersonality))]
public class DragonsPersonalityEditor : Editor
{
    private static float _amount = 20;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        DragonsPersonality myScript = (DragonsPersonality) target;
        EditorGUILayout.LabelField("Mood", myScript.Mood.ToString());
        
        _amount = EditorGUILayout.FloatField("Rage Amount", _amount);


        if (!EditorApplication.isPlaying)
            return;
        
        if (GUILayout.Button("AddRage"))
        {
            myScript.AddRage(_amount);
            Debug.Log("Added Rage: " + _amount);
        }  
        
        if (GUILayout.Button("LoadTexts"))
        {
            myScript.LoadTexts();
        }
        
        if (GUILayout.Button("GetRandomText"))
        {
            Debug.Log(myScript.GetRandomText());
        }  
    }
}