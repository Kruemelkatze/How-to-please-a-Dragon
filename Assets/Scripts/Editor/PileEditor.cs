using UnityEngine;
using System.Collections;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(Pile))]
public class PileEditor : Editor
{
    private static int _amount = 200;
    private static string _item;
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


        _item = EditorGUILayout.TextField("Loot Id", _item);
           
        if (GUILayout.Button("Set Loot"))
        {
            var loot = LootDropper.Instance.ItemDefinitions.FirstOrDefault(x => x.Id == _item);
            if (loot != null)
            {
                myScript.SetLoot(loot);
            }
        }
        
        if (GUILayout.Button("Set Random Loot"))
        {
            var loot = LootDropper.Instance.GetRandomItem();
            myScript.SetLoot(loot);
        }
    }
}