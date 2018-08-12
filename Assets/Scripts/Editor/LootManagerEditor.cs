using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LootManager))]
public class LootManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!EditorApplication.isPlaying)
            return;

        LootManager myScript = (LootManager) target;
        if (GUILayout.Button("Show Modal"))
        {
            myScript.ShowModal();
        }

        if (GUILayout.Button("Hide Modal"))
        {
            myScript.HideModal();
        }
    }
}