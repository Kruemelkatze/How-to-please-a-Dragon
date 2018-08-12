using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        var totalChance = myScript.ItemDefinitions.Sum(x => x.Chance).ToString();
        EditorGUILayout.LabelField("Total Weight: ", totalChance);


        if (GUILayout.Button("Random Item"))
        {
            var item = myScript.GetRandomItem();
            Debug.Log("Random Item:");
            Debug.Log(JsonUtility.ToJson(item));
        }
    }
}