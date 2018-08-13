using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CameraShake))]
public class CameraShakeEditor : Editor
{
    private static int _amount = 200;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!EditorApplication.isPlaying)
            return;

        CameraShake myScript = (CameraShake) target;

        if (GUILayout.Button("Shake"))
        {
            myScript.Shake(Dragon.Instance.ShakeDuration);
        }
    }
}