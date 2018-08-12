using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Pile))]
public class PileEditor : Editor
{
    private static int _amount = 200;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        if (!EditorApplication.isPlaying)
            return;

        Pile myScript = (Pile) target;
        _amount = EditorGUILayout.IntField("Amount to Edit", _amount);
        if (GUILayout.Button("+ Amount"))
        {
            myScript.Add(_amount);
        }

        if (GUILayout.Button("- Amount"))
        {
            var actualSubtracted = myScript.Subtract(_amount);
            Debug.Log("Subtracted " + actualSubtracted);
        }
        
        if (GUILayout.Button("Set Random Loot"))
        {
            var loot = LootDropper.Instance.GetRandomItem();
            myScript.SetLoot(loot);
        }
    }
}